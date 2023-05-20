using CleanArchitectureNetCore.Application.Common;
using FluentValidation;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCoursesVisualizer;

public class GetCoursesVisualizerQueryValidator : AbstractValidator<GetCoursesVisualizerQuery>
{
    private readonly Sort<CourseModel> _sort;

    public GetCoursesVisualizerQueryValidator(Sort<CourseModel> sort)
    {
        _sort = sort;

        RuleFor(x => x)
            .Must(IsOrderByValid)
            .WithMessage("OrderBy no valid");
    }

    private bool IsOrderByValid(GetCoursesVisualizerQuery query)
    {
        return !_sort.ContainOrderBy() || (_sort.ContainOrderBy() && _sort.IsValid());
    }
}