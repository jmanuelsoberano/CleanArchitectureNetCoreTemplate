namespace CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCourseVisualizer;

public interface IGetCourseVisualizerQueryRepositoryFacade
{
    Task<GetCourseVisualizerQueryResponse> GetCourseAsync(GetCourseVisualizerQuery query);
    Task<bool> ExistsCourseAsync(GetCourseVisualizerQuery query);
}