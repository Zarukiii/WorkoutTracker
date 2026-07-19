using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace WorkoutTracker.Models
{
    public abstract class BaseModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
