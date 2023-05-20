using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.Domain.Courses;
using FluentValidation;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    private readonly IRepository<Course> _repository;

    public CreateCourseCommandValidator(IRepository<Course> repository)
    {
        _repository = repository;

        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(250).WithMessage("{PropertyName} must not exceed 250 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters");

        RuleFor(x => x.Level)
            .Must(Course.IsValidLevel).WithMessage("{PropertyName} is invalid");

        RuleFor(x => x.Id)
            .MustAsync(IsUniqueCourseId)
            .WithMessage("{PropertyName} already exists");
    }

    private async Task<bool> IsUniqueCourseId(Guid courseId, CancellationToken token)
    {
        return !await _repository.ExistByIdAsync(courseId);
    }
}