using System.Reflection;
using ExerciseTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Contexts;

public class ExerciseTrackerContext : DbContext
{
    public ExerciseTrackerContext(DbContextOptions<ExerciseTrackerContext> options) : base(options)
    {

    }

    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
