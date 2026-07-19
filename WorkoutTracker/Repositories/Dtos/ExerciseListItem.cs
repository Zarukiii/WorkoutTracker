using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutTracker.Styles;

namespace WorkoutTracker.Repositories.Dtos
{
    public partial class ExerciseListItem : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Equipment { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string PrimaryMuscle { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FavoriteGlyph))]
        private bool _isFavorite;
        public bool IsCustom { get; set; }

        public string Subtitle => string.Join(" • ",
            new[] { PrimaryMuscle, Equipment }.Where(s => !string.IsNullOrWhiteSpace(s)));

        public string FavoriteGlyph => IsFavorite
            ? MaterialIcons.Favorite
            : MaterialIcons.FavoriteOutline;
    }
}
