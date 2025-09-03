using ExerciseTracker.Domain.Models;

namespace ExerciseTracker.UnitTests.Models;

public class CategoryTests
{
    [Fact]
    public void Constructor_WithValidName_ShouldInitializeProperites()
    {
        var name = "Strength";

        var category = new Category(name);

        Assert.NotEqual(Guid.Empty, category.Id);
        Assert.Equal(name, category.Name);
        Assert.True(category.IsActive);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidName_ShouldThrowArgumentException(string? invalidName)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Category(invalidName!));
        Assert.Equal("Category name is required.", exception.Message);
    }

    [Fact]
    public void ChangeName_WithValidName_ShouldUpdateName()
    {
        var category = new Category("Strength");
        var newName = "Cardio";

        category.ChangeName(newName);

        Assert.Equal(newName, category.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ChangeName_WithInvalidName_ShouldThrowArgumentException(string? invalidName)
    {
        var category = new Category("Strength");

        var exception = Assert.Throws<ArgumentException>(() => category.ChangeName(invalidName!));
        Assert.Equal("Name cannot be empty.", exception.Message);
    }
}
