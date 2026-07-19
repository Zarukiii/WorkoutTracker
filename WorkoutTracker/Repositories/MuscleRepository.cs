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

        public async Task<IEnumerable<Muscle>> GetListAsync()
        {
            var db = await _dbService.GetConnectionAsync();

            string query = @"
                SELECT 
                    m.Id,
                    m.Name,
                    m.IsFavorite,
                    m.IsCustom,
                    m.CreatedAt,
                    m.UpdatedAt
                FROM Muscles m
                WHERE m.IsActive = 1";

            return await db.QueryAsync<Muscle>(query);
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

        public async Task<bool> SetFavoriteAsync(int muscleId)
        {
            var db = await _dbService.GetConnectionAsync();

            string query = @"
                UPDATE Muscles
                SET IsFavorite = CASE WHEN IsFavorite = 1 THEN 0 ELSE 1 END
                WHERE Id = ?";

            int rowsAffected = await db.ExecuteAsync(query, muscleId);
            return rowsAffected > 0;
        }
    }
}
