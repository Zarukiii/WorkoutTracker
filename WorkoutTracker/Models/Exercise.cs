using WorkoutTracker.Models.Enums;

namespace WorkoutTracker.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Force? Force { get; set; }
        public DifficultyLevel? Level { get; set; }
        public Mechanic? Mechanic { get; set; }
        public Equipment? Equipment { get; set; }
        public List<Muscle> PrimaryMuscles { get; set; } = [];
        public List<Muscle> SecondaryMuscles { get; set; } = [];
        public List<string> Instructions { get; set; } = [];
        public Category? Category { get; set; }
        public bool IsActive { get; set; }
        public bool IsCustom { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
