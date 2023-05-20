namespace CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCoursesVisualizer;

public interface IGetCoursesVisualizerQueryRepository
{
    Task<GetCoursesVisualizerQueryResponse> GetCoursesAsync(GetCoursesVisualizerQuery query);
}