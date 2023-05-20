using System.Diagnostics;
using CleanArchitectureNetCore.Application.Contracts;
using CleanArchitectureNetCore.DataAccess.Commands.Entities.Clips;
using CleanArchitectureNetCore.DataAccess.Commands.Entities.Courses;
using CleanArchitectureNetCore.DataAccess.Commands.Entities.Modules;
using CleanArchitectureNetCore.Domain.Common;
using CleanArchitectureNetCore.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureNetCore.DataAccess.Commands.Shared;

public class DatabaseContext : DbContext, IDatabaseContext
{
    private readonly ILoggedInUserService _loggedInUserService;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, ILoggedInUserService loggedInUserService) :
        base(options)
    {
        _loggedInUserService = loggedInUserService;
    }

    public DbSet<Course> Courses { get; set; } = null!;

#pragma warning disable CS0108, CS0114
    public DbSet<T> Set<T>() where T : class, IEntity<Guid>
#pragma warning restore CS0108, CS0114
    {
        return base.Set<T>();
    }

    public async Task SaveAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = _loggedInUserService.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                    break;
            }

        await SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new ModuleConfiguration());
        modelBuilder.ApplyConfiguration(new ClipConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }
}