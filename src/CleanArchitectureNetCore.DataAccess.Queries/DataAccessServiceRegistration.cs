using CleanArchitectureNetCore.DataAccess.Queries.Features.Catalogs;
using CleanArchitectureNetCore.DataAccess.Queries.Features.Library;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureNetCore.DataAccess.Queries;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddCatalogsServices();
        services.AddLibraryServices();

        return services;
    }
}