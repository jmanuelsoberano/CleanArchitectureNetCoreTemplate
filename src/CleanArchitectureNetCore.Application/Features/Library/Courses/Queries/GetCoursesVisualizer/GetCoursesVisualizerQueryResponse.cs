using System.Net.Security;

namespace CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCoursesVisualizer;

public record GetCoursesVisualizerQueryResponse(IEnumerable<CourseModel> Courses);

public record CourseModel(Guid Id, string Name, string Level, string TotalDuration);