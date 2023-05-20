using CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCoursesVisualizer;
using CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCourseVisualizer;
using CleanArchitectureNetCore.DataAccess.Queries.Features.Library.Courses.GetCoursesVisualizer;
using CleanArchitectureNetCore.DataAccess.Queries.Features.Library.Courses.GetCourseVisualizer;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Library;

public static class LibraryServiceRegistration
{
    public static IServiceCollection AddLibraryServices(this IServiceCollection services)
    {
        services.AddScoped<IGetCoursesVisualizerQueryRepository, GetCoursesVisualizerQueryRepository>();
        services.AddScoped<IGetCourseVisualizerQueryRepository, GetCourseVisualizerQueryRepository>();

        return services;
    }
}