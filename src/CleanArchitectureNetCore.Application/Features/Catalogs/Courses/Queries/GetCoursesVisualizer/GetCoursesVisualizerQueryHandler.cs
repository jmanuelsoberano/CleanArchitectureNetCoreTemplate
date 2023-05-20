using CleanArchitectureNetCore.Application.Common;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCoursesVisualizer;

public class
    GetCoursesVisualizerQueryHandler : IRequestHandler<GetCoursesVisualizerQuery,
        Result<GetCoursesVisualizerQueryRespond>>
{
    private readonly IGetCoursesVisualizerQueryRepository _repository;

    public GetCoursesVisualizerQueryHandler(IGetCoursesVisualizerQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetCoursesVisualizerQueryRespond>> Handle(GetCoursesVisualizerQuery request,
        CancellationToken cancellationToken)
    {
        var sort = new Sort<CourseModel>(request.OrderBy);
        var validationResult =
            await new GetCoursesVisualizerQueryValidator(sort).ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure<GetCoursesVisualizerQueryRespond>(validationResult.ErrorMessages());

        return Result.Success(await _repository.GetCoursesVisualizerAsync(request));
    }
}