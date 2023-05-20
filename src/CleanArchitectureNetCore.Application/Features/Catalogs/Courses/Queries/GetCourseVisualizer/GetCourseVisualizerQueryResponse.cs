namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCourseVisualizer;

public record GetCourseVisualizerQueryResponse(Guid Id, string Name, string Description, string Level,
    List<ModuleModel> Modules);

public record ModuleModel(Guid Id, string Name, int Position, List<ClipModel> Clips);

public record ClipModel(Guid Id, string Name, int Position, TimeSpan Duration, string Url);