using FluentValidation;

namespace CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCourseVisualizer;

public class GetCourseVisualizerQueryValidator : AbstractValidator<GetCourseVisualizerQuery>
{
    private readonly IGetCourseVisualizerQueryRepositoryFacade _repository;

    public GetCourseVisualizerQueryValidator(IGetCourseVisualizerQueryRepositoryFacade repository)
    {
        _repository = repository;
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty");

        RuleFor(x => x)
            .MustAsync(ExistsCourseAsync)
            .WithMessage("Course not found");
    }

    private async Task<bool> ExistsCourseAsync(GetCourseVisualizerQuery query, CancellationToken token)
    {
        return await _repository.ExistsCourseAsync(query);
    }
}