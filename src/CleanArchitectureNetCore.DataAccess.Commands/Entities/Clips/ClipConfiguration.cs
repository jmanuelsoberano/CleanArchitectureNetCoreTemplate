using CleanArchitectureNetCore.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureNetCore.DataAccess.Commands.Entities.Clips;

public class ClipConfiguration : IEntityTypeConfiguration<Clip>
{
    public void Configure(EntityTypeBuilder<Clip> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Position)
            .IsRequired();

        builder.Property(p => p.Duration)
            .IsRequired();

        builder.Property(p => p.Url)
            .IsRequired();
    }
}