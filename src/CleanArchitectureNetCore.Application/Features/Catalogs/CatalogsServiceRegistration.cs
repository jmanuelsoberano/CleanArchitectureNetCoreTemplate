using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCourseVisualizer;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureNetCore.Application.Features.Catalogs;

public static class CatalogsServiceRegistration
{
    public static IServiceCollection AddCatalogsServices(this IServiceCollection service)
    {
        service.AddTransient<IGetCourseVisualizerQueryRepositoryFacade, GetCourseVisualizerQueryRepositoryFacade>();
        service.AddTransient<IUpdateCourseCommandRepositoryFacade, UpdateCourseCommandRepositoryFacade>();

        return service;
    }
}