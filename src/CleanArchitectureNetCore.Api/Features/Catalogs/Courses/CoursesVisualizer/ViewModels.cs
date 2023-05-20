using CleanArchitectureNetCore.Application.Common.Pagination;

namespace CleanArchitectureNetCore.Api.Features.Catalogs.Courses.CoursesVisualizer;

public record CoursesVisualizerVm(IEnumerable<CourseVm> Courses, IPagedList Pagination);

public record CourseVm(Guid Id, string Name, string Description, string Level, int QuantityOfModules);
