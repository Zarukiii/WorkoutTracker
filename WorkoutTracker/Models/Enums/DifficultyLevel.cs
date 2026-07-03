using SQLite;

namespace WorkoutTracker.Models.Enums
{
    [StoreAsText]
    public enum DifficultyLevel
    {
        Beginner = 0,
        Intermediate = 1,
        Expert = 2
    }
}
