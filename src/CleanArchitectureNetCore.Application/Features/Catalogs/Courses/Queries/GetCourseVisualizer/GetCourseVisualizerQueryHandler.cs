using CleanArchitectureNetCore.Application.Common;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCourseVisualizer;

public class GetCourseVisualizerQueryHandler :
    IRequestHandler<GetCourseVisualizerQuery, Result<GetCourseVisualizerQueryResponse>>
{
    private readonly IGetCourseVisualizerQueryRepositoryFacade _repository;

    public GetCourseVisualizerQueryHandler(IGetCourseVisualizerQueryRepositoryFacade repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetCourseVisualizerQueryResponse>> Handle(GetCourseVisualizerQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult = await new GetCourseVisualizerQueryValidator(_repository)
            .ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure<GetCourseVisualizerQueryResponse>(validationResult.ErrorMessages());

        return Result.Success(await _repository.GetCourseAsync(request));
    }
}