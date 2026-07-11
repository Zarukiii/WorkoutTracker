using WorkoutTracker.Data.Abstractions;
using WorkoutTracker.Models;
using WorkoutTracker.Repositories.Abstractions;
using WorkoutTracker.Repositories.Dtos;

namespace WorkoutTracker.Repositories
{
    public class MuscleRepository : IMuscleRepository
    {
        private readonly IDbService _dbService;

        public MuscleRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IEnumerable<Muscle>> GetAllAsync()
        {
            var db = await _dbService.GetConnectionAsync();

            return await db.Table<Muscle>().ToListAsync();
        }

        public async Task<IEnumerable<ExerciseMuscleRow>> GetExerciseMusclesAsync(int? exerciseId = null)
        {
            var db = await _dbService.GetConnectionAsync();

            string query = @"
                SELECT
                    em.ExerciseId,
                    em.MuscleId,
                    em.Role,
                    m.Name    AS MuscleName,
                    m.IsCustom
                FROM ExerciseMuscles em
                LEFT JOIN Muscles m ON m.Id = em.MuscleId";

            if (exerciseId is int exId)
            {
                query += @" WHERE em.ExerciseId = ?";
                return await db.QueryAsync<ExerciseMuscleRow>(query, exerciseId);
            }

            return await db.QueryAsync<ExerciseMuscleRow>(query);
        }
    }
}
