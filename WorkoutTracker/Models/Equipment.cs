using SQLite;

namespace WorkoutTracker.Models
{
    [Table("Equipments")]
    public class Equipment : BaseModel
    {
        [NotNull, MaxLength(100), Collation("NOCASE")]
        [Indexed(Name = "UX_Equipment_Name", Unique = true)]
        public string Name { get; set; } = string.Empty;
        public bool IsCustom { get; set; }
    }
}
