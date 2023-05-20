namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Library.Courses.GetCourseVisualizer;

public record CourseWithModulesAndClipsModel(
    Guid CourseId, string CourseName, string CourseDescription, string CourseLevel,
    Guid ModuleId, string ModuleName, int ModulePosition,
    Guid ClipId, string ClipName, int ClipPosition, string ClipDuration, string ClipUrl,
    string ModuleDuration, string CourseDuration
);

public record CourseResponseModel(Guid Id, string Name, string Description, string Level, string Duration, IEnumerable<ModuleResponseModel> Modules);

public record ModuleResponseModel(Guid Id, string Name, int Position, string Duration, IEnumerable<ClipResponseModel> Clips);

public record ClipResponseModel(Guid Id, string Name, int Position, string Duration, string Url);