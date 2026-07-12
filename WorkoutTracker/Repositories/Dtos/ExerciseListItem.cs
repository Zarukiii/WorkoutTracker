namespace WorkoutTracker.Repositories.Dtos
{
    public class ExerciseListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Equipment { get; set; } = string.Empty;
    }
}
