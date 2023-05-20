namespace CleanArchitectureNetCore.Api.Features.Library.Courses.CoursesVisualizer;

public record CoursesVisualizerVm(IEnumerable<CourseVm> Courses);

public record CourseVm(Guid Id, string Name, string Level, string TotalDuration);
