using ExerciseTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExerciseTracker.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasQueryFilter(e => e.IsActive);
    }
}
