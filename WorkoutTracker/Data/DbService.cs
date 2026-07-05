using SQLite;
using WorkoutTracker.Models;

namespace WorkoutTracker.Data
{
    public class DbService
    {
        private SQLiteAsyncConnection? _db;
        private readonly SemaphoreSlim _initLock = new(1, 1);

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_db is not null)
                return _db;

            await _initLock.WaitAsync();

            try
            {
                if (_db is not null)
                    return _db;

                var connectionString = new SQLiteConnectionString(
                    DbConstants.DbPath,
                    DbConstants.Flags,
                    storeDateTimeAsTicks: true);

                var db = new SQLiteAsyncConnection(connectionString);

                await InitializeAsync(db);

                _db = db;

                return _db;
            }
            finally
            {
                _initLock.Release();
            }
        }

        private static async Task InitializeAsync(SQLiteAsyncConnection db)
        {
            await db.CreateTablesAsync(CreateFlags.None,
                typeof(Category),
                typeof(Equipment),
                typeof(Exercise),
                typeof(ExerciseInstruction),
                typeof(ExerciseMuscle),
                typeof(Muscle),
                typeof(Routine),
                typeof(RoutineExercise),
                typeof(SessionExercise),
                typeof(WorkoutSession),
                typeof(WorkoutSet));

            await DatabaseSeeder.SeedIfEmptyAsync(db);
        }
    }
}
