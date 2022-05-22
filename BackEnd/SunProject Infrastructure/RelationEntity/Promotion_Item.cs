global using SunProject_Infrastructure.Entity;

namespace SunProject_Infrastructure.RelationEntity
{
    public class Promotion_Item
    {
        [Required, Key]
        public int Id { get; set;}
        
        // Foreign Key to Promotion Table
        [Required, MaxLength(13)]
        public string PromotionId { get; set; }

        public Promotion Promotion { get; set; }

        [Required, MaxLength(8)]
        public string ItemId { get; set; }
    }
}
