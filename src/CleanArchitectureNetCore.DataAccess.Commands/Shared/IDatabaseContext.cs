using CleanArchitectureNetCore.Domain.Common;
using CleanArchitectureNetCore.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureNetCore.DataAccess.Commands.Shared;

public interface IDatabaseContext
{
    DbSet<Course> Courses { get; set; }

    DbSet<T> Set<T>() where T : class, IEntity<Guid>;

    Task SaveAsync(CancellationToken cancellationToken = new());
}