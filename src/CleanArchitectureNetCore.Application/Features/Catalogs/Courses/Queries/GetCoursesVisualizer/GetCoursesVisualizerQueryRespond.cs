using CleanArchitectureNetCore.Application.Common.Pagination;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCoursesVisualizer;

public record GetCoursesVisualizerQueryRespond(IEnumerable<CourseModel> Courses, IPagedList Pagination);

public record CourseModel(Guid Id, string Name, string Description, string Level, int QuantityOfModules);