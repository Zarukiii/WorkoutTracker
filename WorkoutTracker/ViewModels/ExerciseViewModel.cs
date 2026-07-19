using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkoutTracker.Repositories.Abstractions;
using WorkoutTracker.Repositories.Dtos;

namespace WorkoutTracker.ViewModels
{
    public partial class ExerciseViewModel : ObservableObject
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ObservableCollection<ExerciseListItem> Exercises { get; } = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _searchText = string.Empty;

        partial void OnSearchTextChanged(string value)
        {
            FilterExercises();
        }

        private IEnumerable<ExerciseListItem> _exercises = Enumerable.Empty<ExerciseListItem>();

        public ExerciseViewModel(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task LoadExercisesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                _exercises = await _exerciseRepository.GetExerciseListAsync();

                FilterExercises();
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task ToggleFavoriteAsync(ExerciseListItem exercise)
        {
            exercise.IsFavorite = !exercise.IsFavorite;

            try
            {
                await _exerciseRepository.SetFavoriteAsync(exercise.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error toggling favorite: {ex.Message}");
                exercise.IsFavorite = !exercise.IsFavorite;
            }
        }

        private void FilterExercises()
        {
            var query = _searchText?.Trim() ?? string.Empty;

            var matches = string.IsNullOrEmpty(query)
                ? _exercises
                : _exercises.Where(e => e.Name.Contains(query, StringComparison.OrdinalIgnoreCase));

            Exercises.Clear();
            foreach (var item in matches)
                Exercises.Add(item);
        }
    }
}
