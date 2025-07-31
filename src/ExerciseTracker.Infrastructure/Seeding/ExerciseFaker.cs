using Bogus;
using ExerciseTracker.Domain.Models;

namespace ExerciseTracker.Infrastructure.Seeding;

public class ExerciseFaker : Faker<Exercise>
{
    public ExerciseFaker(List<Category> categories)
    {
        UseSeed(1234);

        CustomInstantiator(f =>
        {
            var category = f.PickRandom(categories);
            var start = f.Date.Recent(SeedingConstants.NumberOfExercises);
            var end = start.AddMinutes(f.Random.Number(15, 120));
            var description = f.Random.Bool(0.8f)
                ? $"{category.Name} on {start.ToShortDateString()} from {start.ToShortTimeString()}h to {end.ToShortTimeString()}h"
                : null;

            return new Exercise(category.Id, start, end, description, category);
        });
    }
}
