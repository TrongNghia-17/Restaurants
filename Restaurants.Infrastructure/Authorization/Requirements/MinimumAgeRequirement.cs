using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Constants.Requirements;

public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;
}
