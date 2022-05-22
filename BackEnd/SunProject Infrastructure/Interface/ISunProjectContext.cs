using Microsoft.EntityFrameworkCore;

namespace SunProject_Infrastructure.Interface
{
    public interface ISunProjectContext
    {
        public DbSet<Stores> Store { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Promotion_Store> Promotion_Store { get; set; }
        public DbSet<Promotion_Item> Promotion_Item { get; set; }
    }
}
