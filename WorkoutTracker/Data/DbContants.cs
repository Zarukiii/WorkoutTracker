using SQLite;

namespace WorkoutTracker.Data
{
    public class DbContants
    {
        public const string DbFilename = "WorkoutTracker.db3";

        public const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        public static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbFilename);
    }
}
