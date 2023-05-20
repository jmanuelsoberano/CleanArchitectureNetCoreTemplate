namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCourseVisualizer;

public interface IGetCourseVisualizerQueryRepository
{
    Task<GetCourseVisualizerQueryResponse> GetCourseAsync(GetCourseVisualizerQuery query);
}