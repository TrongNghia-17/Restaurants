using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = [
             new ()
             {
                Name = "KFC",
                Description = "Famous for fried chicken.",
                Category = "Fast Food",
                ContactEmail = "contact@kfc.com",
                ContactNumber = "123-456-7890",
                HasDelivery = true,
                Address = new Address
                {
                    City = "New York",
                    PostalCode = "10001",
                    Street = "123 Main St"
                },
                Dishes =
                [
                    new Dish
                    {
                        Name = "Chicken Bucket",
                        Description = "A bucket full of crispy fried chicken.",
                        Price = 25.99m,
                        RestaurantId = 1
                    },
                    new Dish
                    {
                        Name = "French Fries",
                        Description = "Golden and crispy fries.",
                        Price = 3.99m,
                        RestaurantId = 1
                    }
                ]
             },
             new()
             {
                Name = "McDonald's",
                Description = "Global fast-food chain.",
                Category = "Fast Food",
                ContactEmail = "info@mcdonalds.com",
                ContactNumber = "987-654-3210",
                HasDelivery = true,
                Address = new Address
                {
                    City = "Los Angeles",
                    PostalCode = "90001",
                    Street = "456 Elm St"
                },
                Dishes = new List<Dish>()
             }
        ];

        return restaurants;
    }
}
