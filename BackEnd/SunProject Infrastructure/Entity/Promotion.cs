global using System.ComponentModel.DataAnnotations;

namespace SunProject_Infrastructure.Entity
{
    public class Promotion
    {
        [Required, Key, MaxLength(13)]
        public string Id { get; set; }

        [Required, MaxLength(30)]
        public string Description { get; set; }

        [Required, MaxLength(1)]
        public string Type { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

    }
}
