﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Constants;
using Restaurants.Infrastructure.Constants.Requirements;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Authorization.Requirements;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantsDbContext>(options => 
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "ThaiLan", "TrungQuoc"))
            .AddPolicy(PolicyNames.AtLeast20, builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
            .AddPolicy(PolicyNames.CreateAtleast2Restaurants, builder => builder.AddRequirements(new CreateMultipleRestaurantsRequirement(2)));

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, CreateMultipleRestaurantsRequirementHandler>();
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
    }
}
