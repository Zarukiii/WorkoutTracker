using SQLite;

namespace WorkoutTracker.Models
{
    [Table("Categories")]
    public class Category : BaseModel
    {
        [NotNull, MaxLength(100), Collation("NOCASE")]
        [Indexed(Name = "UX_Categories_Name", Unique = true)]
        public string Name { get; set; } = string.Empty;

        public bool IsCustom { get; set; }
    }
}
