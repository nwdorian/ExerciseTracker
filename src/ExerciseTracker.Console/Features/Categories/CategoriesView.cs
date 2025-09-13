using ExerciseTracker.Console.Common.Input;
using ExerciseTracker.Console.Features.Categories.Abstractions;
using ExerciseTracker.Contracts.V1.Categories.Requests;
using Spectre.Console;

namespace ExerciseTracker.Console.Features.Categories;

public class CategoriesView : ICategoriesView
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesView(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    public async Task GetAllCategories()
    {
        var categories = await _categoriesService.GetAllCategories();
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No categories found[/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }
        CategoriesTables.DisplayCategoriesTable(categories);

        var input = UserInput.PromptPositiveInteger("Enter category Id [grey](or enter 0 to exit)[/]:", allowZero: true);
        var index = UserInput.GetValidListIndex(input, categories);
        if (index == -1)
        {
            return;
        }

        var category = await _categoriesService.GetCategoryById(categories[index].Id);
        if (category.Id == Guid.Empty)
        {
            return;
        }
        CategoriesTables.DisplayCategoryExercises(category);
        UserInput.PromptAnyKeyToContinue();
    }

    public async Task CreateCategory()
    {
        AnsiConsole.MarkupLine("[blue]Creating a new category...[/]");
        AnsiConsole.MarkupLine("[grey]Enter information or 0 to exit.[/]");
        var categoryName = UserInput.PromptString("Category name:", allowEmpty: false);
        if (categoryName == "0")
        {
            return;
        }

        var category = new CreateCategoryRequest()
        {
            Name = categoryName
        };
        CategoriesTables.DisplayCreateCategoryDetails(category);

        if (await AnsiConsole.ConfirmAsync("Are you sure you want to create a new category?"))
        {
            await _categoriesService.CreateCategory(category);
        }
    }

    public async Task DeleteCategory()
    {
        var categories = await _categoriesService.GetAllCategories();
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No categories found[/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }

        CategoriesTables.DisplayCategoriesTable(categories);
        var input = UserInput.PromptPositiveInteger("Enter category Id to delete [grey](or enter 0 to exit)[/]:", allowZero: true);
        var index = UserInput.GetValidListIndex(input, categories);
        if (index == -1)
        {
            return;
        }

        var category = categories.ElementAtOrDefault(index);
        if (category is null)
        {
            return;
        }
        CategoriesTables.DisplayCategoryDetails(category);
        if (await AnsiConsole.ConfirmAsync("Are you sure you want to delete this category?"))
        {
            await _categoriesService.DeleteCategory(category.Id);
        }
    }

    public async Task UpdateCategory()
    {
        var categories = await _categoriesService.GetAllCategories();
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No categories found[/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }

        CategoriesTables.DisplayCategoriesTable(categories);
        var input = UserInput.PromptPositiveInteger("Enter category Id to update [grey](or enter 0 to exit)[/]:", allowZero: true);
        var index = UserInput.GetValidListIndex(input, categories);
        if (index == -1)
        {
            return;
        }

        var category = categories.ElementAtOrDefault(index);
        if (category is null)
        {
            return;
        }
        CategoriesTables.DisplayCategoryDetails(category);

        AnsiConsole.MarkupLine("[grey]Enter new information or leave empty to skip.[/]");
        var name = UserInput.PromptString("New name:", allowEmpty: true);
        if (string.IsNullOrWhiteSpace(name))
        {
            AnsiConsole.MarkupLine("[red]No changes to update![/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }

        var categoryUpdate = new UpdateCategoryRequest()
        {
            Name = name
        };

        if (await AnsiConsole.ConfirmAsync("Are you sure you want to save the changes?"))
        {
            await _categoriesService.UpdateCategory(category.Id, categoryUpdate);
        }
    }
}
