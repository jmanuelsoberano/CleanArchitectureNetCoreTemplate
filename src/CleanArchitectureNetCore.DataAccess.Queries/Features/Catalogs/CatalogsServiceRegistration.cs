using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCoursesVisualizer;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCourseVisualizer;
using CleanArchitectureNetCore.DataAccess.Queries.Features.Catalogs.Courses.GetCoursesVisualizer;
using CleanArchitectureNetCore.DataAccess.Queries.Features.Catalogs.Courses.GetCourseVisualizer;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Catalogs;

public static class CatalogsServiceRegistration
{
    public static IServiceCollection AddCatalogsServices(this IServiceCollection services)
    {
        services.AddScoped<IGetCourseVisualizerQueryRepository, GetCourseVisualizerQueryRepository>();
        services.AddScoped<IGetCoursesVisualizerQueryRepository, GetCoursesVisualizerQueryRepository>();

        return services;
    }
}