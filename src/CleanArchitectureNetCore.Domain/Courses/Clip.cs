using CleanArchitectureNetCore.Domain.Common;

namespace CleanArchitectureNetCore.Domain.Courses;

public class Clip : AuditableEntity, IEntity<Guid>
{
    public string Name { get; set; }
    public int Position { get; set; }
    public TimeSpan Duration { get; set; }
    public string Url { get; set; }
    public Guid ModuleId { get; set; }
    public Guid Id { get; set; }
}