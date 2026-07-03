using SQLite;

namespace WorkoutTracker.Models
{
    [Table("Routines")]
    public class Routine : BaseModel
    {
        [NotNull, MaxLength(100), Collation("NOCASE")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Ignore]
        public List<RoutineExercise> Exercises { get; set; } = [];
    }
}
