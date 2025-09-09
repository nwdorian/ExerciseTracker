using ExerciseTracker.Console.Abstractions.Services;
using ExerciseTracker.Console.Clients;
using ExerciseTracker.Console.Input;
using ExerciseTracker.Contracts.V1.Categories;
using ExerciseTracker.Contracts.V1.Categories.Requests;
using ExerciseTracker.Contracts.V1.Categories.Responses;
using ExerciseTracker.Contracts.V1.Exercises;
using Spectre.Console;

namespace ExerciseTracker.Console.Services;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesClient _categoriesClient;

    public CategoriesService(ICategoriesClient categoriesClient)
    {
        _categoriesClient = categoriesClient;
    }

    public async Task<GetAllCategoriesResponse> GetAllCategories()
    {
        var categories = new GetAllCategoriesResponse(new List<CategoryRecord>());
        try
        {
            var response = await _categoriesClient.GetAllCategories();

            if (!response.IsSuccessful)
            {
                var errorMessage = string.IsNullOrWhiteSpace(response.Error.Content)
                    ? response.Error.Message
                    : response.Error.Content;

                AnsiConsole.MarkupLine($"[red]{errorMessage}[/]");
                UserInput.PromptAnyKeyToContinue();
                return categories;
            }
            categories = response.Content;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
        return categories;
    }

    public async Task<GetCategoryByIdResponse> GetCategoryById(Guid id)
    {
        var category = new GetCategoryByIdResponse(Guid.Empty, string.Empty, new List<ExerciseShallowRecord>());
        try
        {
            var response = await _categoriesClient.GetCategoryById(id);

            if (!response.IsSuccessful)
            {
                var errorMessage = string.IsNullOrWhiteSpace(response.Error.Content)
                    ? response.Error.Message
                    : response.Error.Content;

                AnsiConsole.MarkupLine($"[red]{errorMessage}[/]");
                UserInput.PromptAnyKeyToContinue();
                return category;
            }
            category = response.Content;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
        return category;
    }

    public async Task CreateCategory(CreateCategoryRequest request)
    {
        try
        {
            var response = await _categoriesClient.CreateCategory(request);

            if (!response.IsSuccessful)
            {
                var errorMessage = string.IsNullOrWhiteSpace(response.Error.Content)
                    ? response.Error.Message
                    : response.Error.Content;

                AnsiConsole.MarkupLine($"[red]{errorMessage}[/]");
                UserInput.PromptAnyKeyToContinue();
            }

            AnsiConsole.MarkupLine("[green]New category created successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }

    public async Task DeleteCategory(Guid id)
    {
        try
        {
            var response = await _categoriesClient.DeleteCategory(id);

            if (!response.IsSuccessful)
            {
                var errorMessage = string.IsNullOrWhiteSpace(response.Error.Content)
                    ? response.Error.Message
                    : response.Error.Content;

                AnsiConsole.MarkupLine($"[red]{errorMessage}[/]");
                UserInput.PromptAnyKeyToContinue();
            }

            AnsiConsole.MarkupLine("[green]Category deleted successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }

    public async Task UpdateCategory(Guid id, UpdateCategoryRequest request)
    {
        try
        {
            var response = await _categoriesClient.UpdateCategory(id, request);

            if (!response.IsSuccessful)
            {
                var errorMessage = string.IsNullOrWhiteSpace(response.Error.Content)
                    ? response.Error.Message
                    : response.Error.Content;

                AnsiConsole.MarkupLine($"[red]{errorMessage}[/]");
                UserInput.PromptAnyKeyToContinue();
            }

            AnsiConsole.MarkupLine("[green]Category updated successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }
}
