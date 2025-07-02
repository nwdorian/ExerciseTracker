using System;
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

        builder
            .HasMany(t => t.Exercises)
            .WithOne(e => e.Category);

        builder.HasQueryFilter(e => e.IsActive);
    }
}
