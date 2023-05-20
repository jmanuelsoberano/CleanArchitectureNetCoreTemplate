using CleanArchitectureNetCore.Domain.Common;

namespace CleanArchitectureNetCore.Domain.Courses;

public class Module : AuditableEntity, IEntity<Guid>
{
    public string Name { get; set; }
    public int Position { get; set; }
    public Guid CourseId { get; set; }
    public IEnumerable<Clip> Clips { get; set; }
    public Guid Id { get; set; }
}