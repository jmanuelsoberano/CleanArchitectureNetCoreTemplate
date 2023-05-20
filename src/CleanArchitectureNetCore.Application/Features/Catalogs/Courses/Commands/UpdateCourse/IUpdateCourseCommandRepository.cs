namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;

public interface IUpdateCourseCommandRepository
{
    Task UpdateCourseAsync(UpdateCourseCommand request);
}