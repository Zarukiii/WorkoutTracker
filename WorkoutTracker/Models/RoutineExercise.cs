using SQLite;

namespace WorkoutTracker.Models
{
    [Table("RoutineExercises")]
    public class RoutineExercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int RoutineId { get; set; }

        [Indexed]
        public int ExerciseId { get; set; }

        public int SortOrder { get; set; }

        [Ignore]
        public Exercise? Exercise { get; set; }
    }
}
