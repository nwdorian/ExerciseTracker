using FluentValidation;

namespace ExerciseTracker.Contracts.V1.Exercises.Requests;

public class UpdateExerciseValidator : AbstractValidator<UpdateExerciseRequest>
{
    public UpdateExerciseValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category Id is required");

        RuleFor(x => x.Start)
            .NotEmpty().WithMessage("Start date is required")
            .Must(BeInPast).WithMessage("Start date can't be in the future")
            .LessThan(x => x.End).WithMessage("Start date must be before end date");

        RuleFor(x => x.End)
            .NotEmpty().WithMessage("End date is required")
            .Must(BeInPast).WithMessage("End date can't be in the future")
            .GreaterThan(x => x.Start).WithMessage("End date must be after start date");
    }

    private static bool BeInPast(DateTime date) => date < DateTime.Now;
}
