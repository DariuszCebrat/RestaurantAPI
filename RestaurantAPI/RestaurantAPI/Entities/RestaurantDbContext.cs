using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Services;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext:DbContext
    {
        private readonly string _connectionString = "Server=.;Database=RestaurantDb;Trusted_Connection=True";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>().Property(r => r.Name).IsRequired().HasMaxLength(25);

            modelBuilder.Entity<Dish>().Property(r => r.Name).IsRequired();
            new DbInitializer(modelBuilder).Seed();
          

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);


        }
    }
}
