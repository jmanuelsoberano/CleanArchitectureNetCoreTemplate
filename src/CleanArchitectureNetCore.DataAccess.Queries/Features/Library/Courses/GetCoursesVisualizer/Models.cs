namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Library.Courses.GetCoursesVisualizer;

public record CourseWithTotalDurationModel(Guid Id, string Name, string Level, string TotalDuration);