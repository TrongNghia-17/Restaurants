using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

internal class CreateMultipleRestaurantsRequirementHandler(IRestaurantsRepository restaurantsRepository, IUserContext userContext) : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        var restaurants = await restaurantsRepository.GetAllAsync();

        var userRestaurantsCreate = restaurants.Count(r => r.OwnerId == currentUser!.Id);

        if (userRestaurantsCreate >= requirement.MinimumRestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
