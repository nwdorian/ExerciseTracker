namespace ExerciseTracker.Application.Contracts.Categories.Commands;

public record class UpdateCategoryCommand(Guid CategoryId, string NewName);
