using CleanArchitectureNetCore.Application.Features.Catalogs;
using CleanArchitectureNetCore.Application.Features.Library;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureNetCore.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        services.AddCatalogsServices();
        services.AddLibraryServices();

        return services;
    }
}