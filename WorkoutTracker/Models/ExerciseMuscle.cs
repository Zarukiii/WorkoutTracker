using SQLite;
using WorkoutTracker.Models.Enums;

namespace WorkoutTracker.Models
{
    [Table("ExerciseMuscles")]
    public class ExerciseMuscle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Name = "UX_ExerciseMuscles_Exercise_Muscle", Order = 1, Unique = true)]
        public int ExerciseId { get; set; }

        [Indexed(Name = "UX_ExerciseMuscles_Exercise_Muscle", Order = 2, Unique = true)]
        public int MuscleId { get; set; }

        public MuscleRole Role { get; set; }
    }
}
