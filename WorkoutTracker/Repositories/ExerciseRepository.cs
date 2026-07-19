using WorkoutTracker.Data.Abstractions;
using WorkoutTracker.Models;
using WorkoutTracker.Models.Enums;
using WorkoutTracker.Repositories.Abstractions;
using WorkoutTracker.Repositories.Dtos;

namespace WorkoutTracker.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly IDbService _dbService;

        private readonly IMuscleRepository _muscleRepository;

        public ExerciseRepository(IDbService dbService, IMuscleRepository muscleRepository)
        {
            _dbService = dbService;
            _muscleRepository = muscleRepository;
        }

        public async Task<Exercise> GetDetailsAsync(int exerciseId)
        {
            var db = await _dbService.GetConnectionAsync();

            var exercise = await db.FindAsync<Exercise>(exerciseId);
            if (exercise == null)
                return null;

            if (exercise.EquipmentId is int eqId)
                exercise.Equipment = await db.FindAsync<Equipment>(eqId);

            if (exercise.CategoryId is int catId)
                exercise.Category = await db.FindAsync<Category>(catId);

            var exerciseMuscles = await _muscleRepository.GetExerciseMusclesAsync(exerciseId);
            var exerciseInstructions = await GetExerciseInstructionsAsync(exerciseId);

            foreach (var row in exerciseMuscles)
            {
                if (row.MuscleName is null)
                    continue;

                var muscle = new Muscle
                {
                    Id = row.MuscleId,
                    Name = row.MuscleName,
                    IsCustom = row.IsCustom
                };

                if (row.Role == MuscleRole.Primary)
                    exercise.PrimaryMuscles.Add(muscle);
                else
                    exercise.SecondaryMuscles.Add(muscle);
            }

            foreach(var instruction in exerciseInstructions)
            {
                if (instruction.Description is null)
                    continue;

                exercise.Instructions.Add(instruction);
            }

            return exercise;
        }

        public async Task<IEnumerable<ExerciseListItem>> GetExerciseListAsync()
        {
            var db = await _dbService.GetConnectionAsync();

            const string query = @"
                SELECT 
                    ex.Id,
                    ex.Name,
                    cat.Name AS Category,
                    eq.Name AS Equipment,
                    m.Name AS PrimaryMuscle
                FROM Exercises ex
                LEFT JOIN Categories cat
                    ON ex.CategoryId = cat.Id
                LEFT JOIN Equipments eq
                    ON ex.EquipmentId = eq.Id
                LEFT JOIN ExerciseMuscles em
                    ON ex.Id = em.ExerciseId
                LEFT JOIN Muscles m
                    ON em.MuscleId = m.Id
                WHERE em.Role = 'Primary'
                ORDER BY ex.Name";

            var exerciseList = await db.QueryAsync<ExerciseListItem>(query);

            return exerciseList;
        }

        public async Task<IEnumerable<ExerciseInstruction>> GetExerciseInstructionsAsync(int? exerciseId = null)
        {
            var db = await _dbService.GetConnectionAsync();

            string query = @"
                SELECT 
                    Id,
                    ExerciseId,
                    Description,
                    StepNumber
                FROM ExerciseInstructions";

            if (exerciseId is int exId)
            {
                query += @"
                    WHERE ExerciseId = ?
                    ORDER BY StepNumber ASC";
                return await db.QueryAsync<ExerciseInstruction>(query, exId);
            }

            query += " ORDER BY StepNumber ASC";

            return await db.QueryAsync<ExerciseInstruction>(query);
        }

        public async Task<bool> SetFavoriteAsync(int exerciseId)
        {
            var db = await _dbService.GetConnectionAsync();

            string query = @"
                UPDATE Exercises
                SET IsFavorite = CASE WHEN IsFavorite = 1 THEN 0 ELSE 1 END
                WHERE Id = ?";

            int rowsAffected = await db.ExecuteAsync(query, exerciseId);
            return rowsAffected > 0;
        }
    }
}
