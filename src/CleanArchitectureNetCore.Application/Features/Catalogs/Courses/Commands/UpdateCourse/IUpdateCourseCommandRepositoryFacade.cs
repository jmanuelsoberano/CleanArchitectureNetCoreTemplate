using CleanArchitectureNetCore.Domain.Courses;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;

public interface IUpdateCourseCommandRepositoryFacade
{
    Task<bool> ExistsCourseAsync(Guid id);
    Task UpdateCourse(UpdateCourseCommand request);
}