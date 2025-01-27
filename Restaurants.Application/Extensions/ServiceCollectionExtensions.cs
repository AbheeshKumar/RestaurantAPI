using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var CurrentAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(CurrentAssembly));

        //Get Assembly of Application into DI
        services.AddAutoMapper(CurrentAssembly);

        services.AddValidatorsFromAssembly(CurrentAssembly)
            .AddFluentValidationAutoValidation();
        
    }
}

