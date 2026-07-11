using WorkoutTracker.Models;
using WorkoutTracker.Repositories.Dtos;

namespace WorkoutTracker.Repositories.Abstractions
{
    public interface IMuscleRepository
    {
        Task<IEnumerable<Muscle>> GetAllAsync();
        Task<IEnumerable<ExerciseMuscleRow>> GetExerciseMusclesAsync(int? exerciseId = null);
    }
}
