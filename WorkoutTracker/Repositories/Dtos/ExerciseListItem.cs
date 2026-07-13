namespace WorkoutTracker.Repositories.Dtos
{
    public class ExerciseListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Equipment { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string PrimaryMuscle { get; set; } = string.Empty;

        public string Subtitle => string.Join(" • ",
            new[] { PrimaryMuscle, Equipment }.Where(s => !string.IsNullOrWhiteSpace(s)));
    }
}
