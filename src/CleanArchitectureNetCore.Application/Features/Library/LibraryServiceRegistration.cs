using CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCourseVisualizer;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureNetCore.Application.Features.Library;

public static class LibraryServiceRegistration
{
    public static IServiceCollection AddLibraryServices(this IServiceCollection service)
    {
        service.AddTransient<IGetCourseVisualizerQueryRepositoryFacade, GetCourseVisualizerQueryRepositoryFacade>();

        return service;
    }
}