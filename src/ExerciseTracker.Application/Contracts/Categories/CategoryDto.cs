using ExerciseTracker.Application.Contracts.Exercises;

namespace ExerciseTracker.Application.Contracts.Categories;

public record class CategoryDto(Guid Id, string Name, List<ExerciseShallowDto> Exercises);
