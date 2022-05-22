using System.ComponentModel.DataAnnotations.Schema;

namespace SunProject_Infrastructure.RelationEntity
{
    public class Promotion_Store
    {
        [Required, Key]
        public int Id { get; set;}

        // Foreign Key to Promotion Table
        [Required, MaxLength(13)]
        public string PromotionId { get; set; }

        public Promotion Promotion { get; set; }

        // Foreign Key to Promotion Table
        [Required, MaxLength(3)]
        public string StoreId { get; set; }

        public Stores Store { get; set; }
    }
}
