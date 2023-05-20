namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCoursesVisualizer;

public interface IGetCoursesVisualizerQueryRepository
{
    Task<GetCoursesVisualizerQueryRespond> GetCoursesVisualizerAsync(GetCoursesVisualizerQuery query);
}