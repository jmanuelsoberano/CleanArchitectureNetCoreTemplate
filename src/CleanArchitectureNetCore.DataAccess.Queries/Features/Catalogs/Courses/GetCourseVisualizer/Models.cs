namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Catalogs.Courses.GetCourseVisualizer;

public record CourseWithModulesAndClipsModel(
    Guid CourseId, string CourseName, string CourseDescription, string CourseLevel,
    Guid ModuleId, string ModuleName, int ModulePosition,
    Guid ClipId, string ClipName, int ClipPosition, TimeSpan ClipDuration, string ClipUrl
);

public record CourseResponseModel(Guid Id, string Name, string Description, string Level, IEnumerable<ModuleResponseModel> Modules);

public record ModuleResponseModel(Guid Id, string Name, int Position, IEnumerable<ClipResponseModel> Clips);

public record ClipResponseModel(Guid Id, string Name, int Position, TimeSpan Duration, string Url);
