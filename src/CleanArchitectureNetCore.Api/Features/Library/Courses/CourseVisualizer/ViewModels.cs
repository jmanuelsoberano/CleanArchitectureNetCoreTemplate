namespace CleanArchitectureNetCore.Api.Features.Library.Courses.CourseVisualizer;

public record CourseVm(Guid Id, string Name, string Description, string Level, string Duration, List<ModuleVm> Modules);

public record ModuleVm(Guid Id, string Name, int Position, string Duration, List<ClipVm> Clips);

public record ClipVm(Guid Id, string Name, int Position, string Duration, string Url);