using ExerciseTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExerciseTracker.Infrastructure.Configurations;


public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercise");

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.HasOne(e => e.Category)
            .WithMany(t => t.Exercises)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired();

        builder.Ignore(e => e.Duration);

        builder.HasQueryFilter(e => e.IsActive);
    }
}

