global using Microsoft.EntityFrameworkCore;
global using SunProject_Infrastructure.RelationEntity;
using Microsoft.Extensions.Configuration;
using SunProject_Infrastructure.Interface;

namespace SunProject_Infrastructure
{
    public class SunProjectContext : DbContext, ISunProjectContext
    {

        public DbSet<Stores> Store { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Promotion_Store> Promotion_Store { get; set; }
        public DbSet<Promotion_Item> Promotion_Item { get; set; }

        private IConfiguration _configuration;

        public SunProjectContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Map entity to table
            modelBuilder.Entity<Stores>().ToTable("Store");
            modelBuilder.Entity<Promotion>().ToTable("Promotion");
            modelBuilder.Entity<Promotion_Item>().ToTable("Promotion_Item");
            modelBuilder.Entity<Promotion_Store>().ToTable("Promotion_Store");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServerConnection"));
        }
    }
}
