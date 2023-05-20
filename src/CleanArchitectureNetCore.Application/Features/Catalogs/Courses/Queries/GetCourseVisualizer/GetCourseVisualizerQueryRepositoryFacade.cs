using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.Domain.Courses;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCourseVisualizer;

public class GetCourseVisualizerQueryRepositoryFacade : IGetCourseVisualizerQueryRepositoryFacade
{
    private readonly IRepository<Course> _courseRepository;
    private readonly IGetCourseVisualizerQueryRepository _queryRepository;

    public GetCourseVisualizerQueryRepositoryFacade(
        IRepository<Course> courseRepository,
        IGetCourseVisualizerQueryRepository queryRepository
    )
    {
        _courseRepository = courseRepository;
        _queryRepository = queryRepository;
    }

    public async Task<GetCourseVisualizerQueryResponse> GetCourseAsync(GetCourseVisualizerQuery query)
    {
        return await _queryRepository.GetCourseAsync(query);
    }

    public async Task<bool> ExistsCourseAsync(GetCourseVisualizerQuery query)
    {
        return await _courseRepository.ExistByIdAsync(query.Id);
    }
}