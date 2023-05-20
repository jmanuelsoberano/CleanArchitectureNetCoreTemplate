using CleanArchitectureNetCore.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureNetCore.DataAccess.Commands.Entities.Courses;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.Level)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(u => u.Modules)
            .WithOne()
            .HasForeignKey(p => p.CourseId);
    }
}