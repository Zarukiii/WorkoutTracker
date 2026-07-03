using SQLite;

namespace WorkoutTracker.Models
{
    [Table("SessionExercises")]
    public class SessionExercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int WorkoutSessionId { get; set; }

        [Indexed]
        public int ExerciseId { get; set; }

        public int SortOrder { get; set; }

        [Ignore]
        public Exercise? Exercise { get; set; }

        [Ignore]
        public List<WorkoutSet> Sets { get; set; } = [];
    }
}
