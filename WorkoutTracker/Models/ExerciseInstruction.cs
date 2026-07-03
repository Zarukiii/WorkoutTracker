using SQLite;

namespace WorkoutTracker.Models
{
    [Table("ExerciseInstructions")]
    public class ExerciseInstruction : BaseModel
    {
        [Indexed]
        public int ExerciseId { get; set; }

        [NotNull]
        public string Description { get; set; } = string.Empty;

        public int StepNumber { get; set; }
    }
}
