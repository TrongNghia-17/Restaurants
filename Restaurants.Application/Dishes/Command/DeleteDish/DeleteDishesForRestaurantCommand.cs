using MediatR;

namespace Restaurants.Application.Dishes.Command.DeleteDish;

public class DeleteDishesForRestaurantCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
}
