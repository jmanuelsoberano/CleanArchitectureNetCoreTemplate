using CleanArchitectureNetCore.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureNetCore.DataAccess.Commands.Entities.Modules;

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Position)
            .IsRequired();

        builder.HasMany(u => u.Clips)
            .WithOne()
            .HasForeignKey(p => p.ModuleId);
    }
}