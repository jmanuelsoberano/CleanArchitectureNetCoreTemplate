namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Catalogs.Courses.GetCoursesVisualizer;

public record CourseWithQuantityOfModulesModel(Guid Id, string Name, string Description, string Level, int QuantityOfModules);