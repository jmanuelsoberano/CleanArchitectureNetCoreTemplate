using CleanArchitectureNetCore.Domain.Courses;
using FluentValidation;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    private readonly IUpdateCourseCommandRepositoryFacade _repository;

    public UpdateCourseCommandValidator(IUpdateCourseCommandRepositoryFacade repository)
    {
        _repository = repository;

        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty");

        RuleFor(x => x.Id)
            .MustAsync(ExistsCourseAsync)
            .WithMessage("Course not found");

        RuleFor(x => x.Level)
            .Must(Course.IsValidLevel).WithMessage("{PropertyName} is invalid");
    }

    private async Task<bool> ExistsCourseAsync(Guid courseId, CancellationToken token)
    {
        return await _repository.ExistsCourseAsync(courseId);
    }
}