namespace CleanArchitectureNetCore.Api.Features.Catalogs.Courses.CourseVisualizer;

public record CourseVm(Guid Id, string Name, string Description, string Level, List<ModuleVm> Modules);

public record ModuleVm(Guid Id, string Name, int Position, List<ClipVm> Clips);

public record ClipVm(Guid Id, string Name, int Position, TimeSpan Duration, string Url);