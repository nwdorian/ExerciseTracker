using ExerciseTracker.Domain.Models;

namespace ExerciseTracker.UnitTests.Models;

public class ExerciseTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldInitializeProperties()
    {
        var category = new Category("Strength");
        var start = DateTime.UtcNow;
        var end = start.AddHours(1);
        var description = "Morning workout";

        var exercise = new Exercise(category.Id, start, end, description, category);

        Assert.NotEqual(Guid.Empty, exercise.Id);
        Assert.Equal(category.Id, exercise.CategoryId);
        Assert.Equal(category, exercise.Category);
        Assert.Equal(start, exercise.Start);
        Assert.Equal(end, exercise.End);
        Assert.Equal(description, exercise.Description);
        Assert.True(exercise.IsActive);
        Assert.Equal(end - start, exercise.Duration);
    }

    [Fact]
    public void Constructor_WithStartAfterEnd_ShouldThrowArgumentException()
    {
        var category = new Category("Strength");
        var start = DateTime.UtcNow;
        var end = start.AddHours(-1);

        var ex = Assert.Throws<ArgumentException>(() =>
            new Exercise(category.Id, start, end, "desc", category));

        Assert.Equal("Start time must be before End time.", ex.Message);
    }

    [Fact]
    public void Constructor_WithNullCategory_ShouldThrowArgumentNullException()
    {
        var start = DateTime.UtcNow;
        var end = start.AddHours(1);

        var ex = Assert.Throws<ArgumentNullException>(() =>
            new Exercise(Guid.NewGuid(), start, end, "desc", null!));

        Assert.Equal("category", ex.ParamName);
    }

    [Fact]
    public void ChangeCategory_ShouldUpdateCategoryAndCategoryId()
    {
        var category = new Category("Strength");
        var newCategory = new Category("Cardio");
        var start = DateTime.UtcNow;
        var end = start.AddHours(1);
        var exercise = new Exercise(category.Id, start, end, "desc", category);

        exercise.ChangeCategory(newCategory);

        Assert.Equal(newCategory, exercise.Category);
        Assert.Equal(newCategory.Id, exercise.CategoryId);
    }

    [Fact]
    public void ChangeCategory_WithNull_ShouldThrowArgumentNullException()
    {
        var category = new Category("Strength");
        var start = DateTime.UtcNow;
        var end = start.AddHours(1);
        var exercise = new Exercise(category.Id, start, end, "desc", category);

        var ex = Assert.Throws<ArgumentNullException>(() => exercise.ChangeCategory(null!));
        Assert.Equal("category", ex.ParamName);
    }

    [Fact]
    public void ChangePeriod_WithValidDates_ShouldUpdateStartAndEnd()
    {
        var category = new Category("Strength");
        var start = DateTime.UtcNow;
        var end = start.AddHours(1);
        var exercise = new Exercise(category.Id, start, end, "desc", category);

        var newStart = start.AddHours(2);
        var newEnd = start.AddHours(3);

        exercise.ChangePeriod(newStart, newEnd);

        Assert.Equal(newStart, exercise.Start);
        Assert.Equal(newEnd, exercise.End);
        Assert.Equal(newEnd - newStart, exercise.Duration);
    }

    [Fact]
    public void ChangePeriod_WithStartAfterEnd_ShouldThrowArgumentException()
    {
        var category = new Category("Strength");
        var start = DateTime.UtcNow;
        var end = start.AddHours(1);
        var exercise = new Exercise(category.Id, start, end, "desc", category);

        var newStart = start.AddHours(2);
        var newEnd = start.AddHours(1);

        var ex = Assert.Throws<ArgumentException>(() => exercise.ChangePeriod(newStart, newEnd));
        Assert.Equal("Start time must be before End time.", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("New description")]
    public void ChangeDescription_ShouldUpdateDescription(string? newDesc)
    {
        var category = new Category("Strength");
        var start = DateTime.UtcNow;
        var end = start.AddHours(1);
        var exercise = new Exercise(category.Id, start, end, "desc", category);

        exercise.ChangeDescription(newDesc);

        Assert.Equal(newDesc, exercise.Description);
    }
}
