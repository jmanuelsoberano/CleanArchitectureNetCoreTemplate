using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.DataAccess.Commands.Shared;
using CleanArchitectureNetCore.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureNetCore.DataAccess.Commands.Entities.Courses;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(IDatabaseContext database) : base(database)
    {
    }

    public async Task<IEnumerable<ListItemForSelect>> GetListItemForSelect()
    {
        return await _database.Courses.Select(s => new ListItemForSelect(s.Name, s.Id.ToString(), null)).ToListAsync();
    }

}