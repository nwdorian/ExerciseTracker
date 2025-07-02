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

        builder.HasQueryFilter(e => e.IsActive);
    }
}

