using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;
using CleanArchitectureNetCore.DataAccess.Commands.Entities.Courses;
using CleanArchitectureNetCore.DataAccess.Commands.Features.Catalogs.Courses.UpdateCourse;
using CleanArchitectureNetCore.DataAccess.Commands.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureNetCore.DataAccess.Commands;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CleanArchitectureNetCoreConnectionString")));

        services.AddScoped<IDatabaseContext, DatabaseContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUpdateCourseCommandRepository, UpdateCourseCommandRepository>();

        return services;
    }
}