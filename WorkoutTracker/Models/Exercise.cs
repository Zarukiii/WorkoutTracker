using SQLite;
using WorkoutTracker.Models.Enums;

namespace WorkoutTracker.Models
{
    [Table("Exercises")]
    public class Exercise : BaseModel
    {
        [NotNull, MaxLength(100), Collation("NOCASE")]
        public string Name { get; set; } = string.Empty;

        public Force? Force { get; set; }
        public DifficultyLevel? Level { get; set; }
        public Mechanic? Mechanic { get; set; }

        [Indexed]
        public int? EquipmentId { get; set; }

        [Ignore]
        public Equipment? Equipment { get; set; }

        [Ignore]
        public List<Muscle> PrimaryMuscles { get; set; } = [];

        [Ignore]
        public List<Muscle> SecondaryMuscles { get; set; } = [];

        [Ignore]
        public List<ExerciseInstruction> Instructions { get; set; } = [];

        [Indexed]
        public int? CategoryId { get; set; }

        [Ignore]
        public Category? Category { get; set; }

        public bool IsCustom { get; set; }
    }
}
