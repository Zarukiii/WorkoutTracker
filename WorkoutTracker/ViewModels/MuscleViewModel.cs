using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutTracker.Models;
using WorkoutTracker.Repositories.Abstractions;

namespace WorkoutTracker.ViewModels
{
    public partial class MuscleViewModel : ObservableObject
    {
        private readonly IMuscleRepository _muscleRepository;

        public ObservableCollection<Muscle> Muscles { get; } = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _searchText = string.Empty;

        partial void OnSearchTextChanged(string value)
        {
            FilterMuscles();
        }

        private IEnumerable<Muscle> _muscles = Enumerable.Empty<Muscle>();

        public MuscleViewModel(IMuscleRepository muscleRepository)
        {
            _muscleRepository = muscleRepository;
        }

        public async Task LoadMusclesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                _muscles = await _muscleRepository.GetListAsync();

                FilterMuscles();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void FilterMuscles()
        {
            var query = _searchText?.Trim() ?? string.Empty;

            var matches = string.IsNullOrEmpty(query)
                ? _muscles
                : _muscles.Where(m => m.Name.Contains(query, StringComparison.OrdinalIgnoreCase));

            Muscles.Clear();
            foreach (var item in matches)
                Muscles.Add(item);
        }
    }
}
