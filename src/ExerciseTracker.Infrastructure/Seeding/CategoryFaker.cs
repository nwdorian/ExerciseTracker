using ExerciseTracker.Domain.Models;

namespace ExerciseTracker.Infrastructure.Seeding;

public static class CategoryFaker
{
    public static List<Category> GetPredefinedCategories()
    {
        return new List<Category>
        {
            new("Running"),
            new("Cycling"),
            new("Walking"),
            new("Swimming"),
            new("Stretching"),
            new("Weightlifting"),
            new("Yoga"),
            new("Pilates"),
            new("Jump Rope"),
            new("Boxing")
        };
    }
}
