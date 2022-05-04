using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Services
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        private IEnumerable<Role> GetRoles ()
        {
            var roles = new List<Role>()
            {
                new Role(){ Id = 3, Name = "User"},
                new Role(){Id = 1,Name ="Manager"},
                new Role(){Id = 2,Name ="Admin"}
            };
            return roles;
        }

        public void Seed()
        {
           
            modelBuilder.Entity<Role>().HasData(
                GetRoles()

                ) ;
            modelBuilder.Entity<Address>().HasData(
                new Address()
                {
                    Id = 2,
                    City = "Karków",
                    Street = "Miejska 10",
                    PostalCode = "30-001"
                }, new Address()
                {
                    Id = 1,
                    City = "Kraków",
                    Street = "Długa 10",
                    PostalCode = "30-001"
                }
                );
            modelBuilder.Entity<Dish>().HasData(

                        new Dish()
                        {
                            Id = 1,
                            Name = "burger",
                            Price = 8.30M,
                            RestaurantId = 1,
                            Description=""

                        },
                         new Dish()
                         {
                             Id = 2,
                             Name = "Cheeseburger",
                             Price = 10.30M,
                             RestaurantId = 1,
                             Description = ""
                         },
                         new Dish()
                         {
                             Id = 3,
                             Name = "Chiken Nuggets",
                             Price = 12.50M,
                             RestaurantId = 1,
                             Description = ""
                         },
                          new Dish()
                          {
                              Id = 4,
                              Name = "salad",
                              Price = 11.30M,
                              RestaurantId = 2,
                              Description = ""
                          },
                         new Dish()
                         {
                             Id = 5,
                             Name = "BigBurger",
                             Price = 10.30M,
                             RestaurantId = 2,
                             Description = ""
                         },
                         new Dish()
                         {
                             Id = 6,
                             Name = "Chiken Nuggets",
                             Price = 13.50M,
                             RestaurantId = 2,
                             Description = ""

                         }
                    );
            modelBuilder.Entity<Restaurant>().HasData(
                    new Restaurant()
                    {
                        Id = 1,
                        Name = "KFC",
                        Description = "KFC american fast food restaurant",
                        ContactEmail = "kfc@gmail.com",
                        HasDelivery = true,
                        Category = "fast food",
                        ContactNumber = "222333444",
                        AddressId=1
                      
                    },
                new Restaurant()
                {
                    Id = 2,
                    Name = "McDonald",
                    Description = "McDonald american fast food restaurant",
                    ContactEmail = "mcdonald@gmail.com",
                    HasDelivery = false,
                    Category = "fast food",
                    ContactNumber = "555333444",
                  AddressId=2
                 
                }

            );
        }
    }
}
