using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
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

                var items = await _exerciseRepository.GetExerciseListAsync();

                Exercises.Clear();
                foreach (var item in items)
                    Exercises.Add(item);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
