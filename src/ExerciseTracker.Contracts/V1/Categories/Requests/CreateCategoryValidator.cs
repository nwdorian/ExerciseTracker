using FluentValidation;

namespace ExerciseTracker.Contracts.V1.Categories.Requests;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Maximum name length i 100 characters");
    }
}
