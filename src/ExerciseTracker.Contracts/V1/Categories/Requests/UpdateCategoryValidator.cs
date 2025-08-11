using FluentValidation;

namespace ExerciseTracker.Contracts.V1.Categories.Requests;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Maximum name length is 100 characters");
    }
}
