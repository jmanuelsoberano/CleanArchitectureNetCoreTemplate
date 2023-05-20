using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCoursesVisualizer;

public class
    GetCoursesVisualizerQueryHandler : IRequestHandler<GetCoursesVisualizerQuery,
        Result<GetCoursesVisualizerQueryResponse>>
{
    private readonly IGetCoursesVisualizerQueryRepository _repository;

    public GetCoursesVisualizerQueryHandler(IGetCoursesVisualizerQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetCoursesVisualizerQueryResponse>> Handle(GetCoursesVisualizerQuery request,
        CancellationToken cancellationToken)
    {
        return Result.Success(await _repository.GetCoursesAsync(request));
    }
}