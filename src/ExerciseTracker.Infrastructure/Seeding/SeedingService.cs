using ExerciseTracker.Domain.Models;
using ExerciseTracker.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Seeding;

public static class SeedingService
{
    public static async Task InitializeAsync(ExerciseTrackerContext context)
    {
        if (await context.Exercises.AnyAsync())
        {
            return;
        }

        await PopulateSeedingDataAsync(context);
    }

    private static async Task PopulateSeedingDataAsync(ExerciseTrackerContext context)
    {
        var categories = CategoryFaker.GetPredefinedCategories();
        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();

        var exerciseFaker = new ExerciseFaker(categories);
        var exercises = new List<Exercise>();
        exercises.AddRange(exerciseFaker.Generate(SeedingConstants.NumberOfExercises));

        context.Exercises.AddRange(exercises);
        await context.SaveChangesAsync();
    }
}
