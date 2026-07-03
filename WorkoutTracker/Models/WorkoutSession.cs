using SQLite;

namespace WorkoutTracker.Models
{
    [Table("WorkoutSessions")]
    public class WorkoutSession : BaseModel
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        public DateTime StartedAt { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        [Ignore]
        public List<SessionExercise> Exercises { get; set; } = [];
    }
}
