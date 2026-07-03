using SQLite;

namespace WorkoutTracker.Models
{
    [Table("Muscles")]
    public class Muscle : BaseModel
    {
        [NotNull, MaxLength(60), Collation("NOCASE")]
        [Indexed(Name = "UX_Muscles_Name", Unique = true)]
        public string Name { get; set; } = string.Empty;

        public bool IsCustom { get; set; }
    }
}
