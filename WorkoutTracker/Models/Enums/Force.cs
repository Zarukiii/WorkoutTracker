using SQLite;

namespace WorkoutTracker.Models.Enums
{
    [StoreAsText]
    public enum Force
    {
        Push = 0,
        Pull = 1,
        Static = 2
    }
}
