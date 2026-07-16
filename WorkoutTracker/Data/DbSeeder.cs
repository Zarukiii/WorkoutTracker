using System.Text.Json;
using SQLite;
using WorkoutTracker.Models;
using WorkoutTracker.Models.Enums;

namespace WorkoutTracker.Data;

public static class DatabaseSeeder
{
    public static async Task SeedIfEmptyAsync(SQLiteAsyncConnection db)
    {
        if (await db.Table<Exercise>().CountAsync() > 0)
            return;

        using var stream = await FileSystem.OpenAppPackageFileAsync("exercises.json");
        var dtos = await JsonSerializer.DeserializeAsync<List<ExerciseDto>>(stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];

        var comparer = StringComparer.OrdinalIgnoreCase;
        var now = DateTime.UtcNow;

        var categories = dtos.Select(d => d.Category)
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Distinct(comparer)
            .Select(n => new Category { Name = n!, CreatedAt = now, IsCustom = false })
            .ToList();

        var equipment = dtos.Select(d => d.Equipment)
            .Where(e => !string.IsNullOrWhiteSpace(e))
            .Distinct(comparer)
            .Select(n => new Equipment { Name = n!, CreatedAt = now, IsCustom = false })
            .ToList();

        var muscles = dtos.SelectMany(d => d.PrimaryMuscles.Concat(d.SecondaryMuscles))
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Distinct(comparer)
            .Select(n => new Muscle { Name = n, CreatedAt = now, IsFavorite = false, IsCustom = false })
            .ToList();

        await db.RunInTransactionAsync(tran =>
        {
            tran.InsertAll(categories, runInTransaction: false);
            tran.InsertAll(equipment, runInTransaction: false);
            tran.InsertAll(muscles, runInTransaction: false);

            var catIds = categories.ToDictionary(c => c.Name, c => c.Id, comparer);
            var equipIds = equipment.ToDictionary(e => e.Name, e => e.Id, comparer);
            var muscIds = muscles.ToDictionary(m => m.Name, m => m.Id, comparer);

            var exerciseMuscles = new List<ExerciseMuscle>();
            var instructions = new List<ExerciseInstruction>();

            foreach (var dto in dtos)
            {
                var exercise = new Exercise
                {
                    Name = dto.Name,
                    Force = ParseEnum<Force>(dto.Force),
                    Level = ParseEnum<DifficultyLevel>(dto.Level),
                    Mechanic = ParseEnum<Mechanic>(dto.Mechanic),
                    CategoryId = string.IsNullOrWhiteSpace(dto.Category) ? null : catIds[dto.Category],
                    EquipmentId = string.IsNullOrWhiteSpace(dto.Equipment) ? null : equipIds[dto.Equipment],
                    IsFavorite = false,
                    IsCustom = false,
                    CreatedAt = now
                };

                tran.Insert(exercise);

                var roles = new Dictionary<int, MuscleRole>();
                foreach (var m in dto.SecondaryMuscles.Where(m => !string.IsNullOrWhiteSpace(m)))
                    roles[muscIds[m]] = MuscleRole.Secondary;
                foreach (var m in dto.PrimaryMuscles.Where(m => !string.IsNullOrWhiteSpace(m)))
                    roles[muscIds[m]] = MuscleRole.Primary;

                exerciseMuscles.AddRange(roles.Select(kvp => new ExerciseMuscle
                {
                    ExerciseId = exercise.Id,
                    MuscleId = kvp.Key,
                    Role = kvp.Value
                }));

                instructions.AddRange(dto.Instructions
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .Select((text, i) => new ExerciseInstruction
                    {
                        ExerciseId = exercise.Id,
                        StepNumber = i + 1,
                        Description = text,
                        CreatedAt = now
                    }));
            }

            tran.InsertAll(exerciseMuscles, runInTransaction: false);
            tran.InsertAll(instructions, runInTransaction: false);
        });
    }

    public class ExerciseDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Force { get; set; }
        public string? Level { get; set; }
        public string? Mechanic { get; set; }
        public string? Equipment { get; set; }
        public List<string> PrimaryMuscles { get; set; } = [];
        public List<string> SecondaryMuscles { get; set; } = [];
        public List<string> Instructions { get; set; } = [];
        public string? Category { get; set; }
        public List<string> Images { get; set; } = [];
    }

    private static TEnum? ParseEnum<TEnum>(string? value) where TEnum : struct
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        return Enum.TryParse<TEnum>(value.Replace(" ", ""), ignoreCase: true, out var result)
            ? result
            : null;
    }
}