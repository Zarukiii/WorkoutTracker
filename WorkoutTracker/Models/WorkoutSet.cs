using SQLite;

namespace WorkoutTracker.Models
{
    [Table("WorkoutSets")]
    public class WorkoutSet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int SessionExerciseId { get; set; }

        public int SetNumber { get; set; }

        public DateTime PerformedAt { get; set; }

        public int? Reps { get; set; }
        public double? Weight { get; set; }
        public int? DurationSeconds { get; set; }
        public double? Distance { get; set; }
    }
}
