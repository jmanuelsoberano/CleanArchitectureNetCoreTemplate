using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Domain.Courses;

namespace CleanArchitectureNetCore.Application.Contracts.Persistence;

public interface ICourseRepository : IRepository<Course>
{
    Task<IEnumerable<ListItemForSelect>> GetListItemForSelect();
}