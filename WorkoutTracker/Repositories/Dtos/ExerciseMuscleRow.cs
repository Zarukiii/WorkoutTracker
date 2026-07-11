using WorkoutTracker.Models.Enums;

namespace WorkoutTracker.Repositories.Dtos
{
    public class ExerciseMuscleRow
    {
        public int ExerciseId { get; set; }
        public int MuscleId { get; set; }
        public string? MuscleName { get; set; } 
        public bool IsCustom { get; set; }
        public MuscleRole Role { get; set; }
    }
}
