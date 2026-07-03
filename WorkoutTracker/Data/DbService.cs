using SQLite;
using WorkoutTracker.Models;

namespace WorkoutTracker.Data
{
    public class DbService
    {
        private SQLiteAsyncConnection? _db;

        private async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_db is not null)
                return _db;

            var connectionString = new SQLiteConnectionString(
                DbContants.DbPath,
                DbContants.Flags,
                storeDateTimeAsTicks: true);

            _db = new SQLiteAsyncConnection(connectionString);

            await _db.CreateTableAsync<Exercise>();
            await _db.CreateTableAsync<RoutineExercise>();
            await _db.CreateTableAsync<Routine>();
            await _db.CreateTableAsync<Equipment>();
            await _db.CreateTableAsync<Category>();
            await _db.CreateTableAsync<Muscle>();
            await _db.CreateTableAsync<ExerciseMuscle>();

            return _db;
        }
    }
}
