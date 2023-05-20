using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.Domain.Courses;
using FluentValidation;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
    private readonly IRepository<Course> _repository;

    public DeleteCourseCommandValidator(IRepository<Course> repository)
    {
        _repository = repository;

        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty");

        RuleFor(x => x.Id)
            .MustAsync(ExistsCourseAsync)
            .WithMessage("Course not found");
    }

    private async Task<bool> ExistsCourseAsync(Guid courseId, CancellationToken token)
    {
        return await _repository.ExistByIdAsync(courseId);
    }
}