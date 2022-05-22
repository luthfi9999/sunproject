using System.ComponentModel.DataAnnotations.Schema;

namespace SunProject_Infrastructure.Entity
{
    public class Stores
    {
        [Required, Key, MaxLength(3), Column("Id")]
        public string Store { get; set; }

        [Required, MaxLength(20), Column("Name")]
        public string Store_Name { get; set; }

    }
}
