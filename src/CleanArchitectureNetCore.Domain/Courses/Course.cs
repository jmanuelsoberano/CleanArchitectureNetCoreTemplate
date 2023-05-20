using CleanArchitectureNetCore.Domain.Common;

namespace CleanArchitectureNetCore.Domain.Courses;

public class Course : AuditableEntity, IEntity<Guid>, IAggregateRoot
{
    public const string LevelBeginner = "beginner";
    public const string LevelIntermediate = "intermediate";
    public const string LevelAdvanced = "advanced";

    public static readonly string[] Levels = { LevelBeginner, LevelIntermediate, LevelAdvanced };


    public string Name { get; set; }
    public string Description { get; set; }
    public string Level { get; set; }
    public IEnumerable<Module> Modules { get; set; }
    public Guid Id { get; set; }

    public static bool IsValidLevel(string level)
    {
        return Levels.Contains(level.ToLower());
    }
}