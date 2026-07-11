using SQLite;

namespace WorkoutTracker.Data.Abstractions
{
    public interface IDbService
    {
        Task<SQLiteAsyncConnection> GetConnectionAsync();
    }
}
