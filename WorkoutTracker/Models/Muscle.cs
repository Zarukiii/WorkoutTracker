using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using WorkoutTracker.Styles;

namespace WorkoutTracker.Models
{
    [Table("Muscles")]
    public partial class Muscle : BaseModel
    {
        [NotNull, MaxLength(60), Collation("NOCASE")]
        [Indexed(Name = "UX_Muscles_Name", Unique = true)]
        public string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FavoriteGlyph))]
        private bool _isFavorite;
        public bool IsCustom { get; set; }

        public string FavoriteGlyph => IsFavorite
            ? MaterialIcons.Favorite
            : MaterialIcons.FavoriteOutline;
    }
}
