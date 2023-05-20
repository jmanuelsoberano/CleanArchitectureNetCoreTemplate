namespace CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCourseVisualizer;

public record GetCourseVisualizerQueryResponse(Guid Id, string Name, string Description, string Level, string Duration,
    List<ModuleModel> Modules);

public record ModuleModel(Guid Id, string Name, int Position, string Duration, List<ClipModel> Clips);

public record ClipModel(Guid Id, string Name, int Position, string Duration, string Url);