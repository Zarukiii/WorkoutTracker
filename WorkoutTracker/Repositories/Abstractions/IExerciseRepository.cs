using WorkoutTracker.Models;
using WorkoutTracker.Repositories.Dtos;

namespace WorkoutTracker.Repositories.Abstractions
{
    public interface IExerciseRepository
    {
        Task<Exercise> GetDetailsAsync(int exerciseId);
        Task<IEnumerable<ExerciseListItem>> GetExerciseListAsync();
        Task<IEnumerable<ExerciseInstruction>> GetExerciseInstructionsAsync(int? exerciseId = null);
        Task<bool> SetFavoriteAsync(int exerciseId);
    }
}
