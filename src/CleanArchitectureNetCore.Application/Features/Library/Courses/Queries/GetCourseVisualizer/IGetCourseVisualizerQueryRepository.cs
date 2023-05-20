namespace CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCourseVisualizer;

public interface IGetCourseVisualizerQueryRepository
{
    Task<GetCourseVisualizerQueryResponse> GetCourseAsync(GetCourseVisualizerQuery query);
}