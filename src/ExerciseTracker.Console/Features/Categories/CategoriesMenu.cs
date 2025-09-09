using ExerciseTracker.Console.Features.Categories.Abstractions;
using Spectre.Console;

namespace ExerciseTracker.Console.Features.Categories;

public class CategoriesMenu
{
    private readonly ICategoriesView _categoriesView;

    public CategoriesMenu(ICategoriesView categoriesView)
    {
        _categoriesView = categoriesView;
    }
    private enum MenuOptions
    {
        GetAll,
        Add,
        Delete,
        Update,
        MainMenu
    }

    public async Task Display()
    {
        var exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            var selection = await AnsiConsole.PromptAsync(
                new SelectionPrompt<MenuOptions>()
                    .Title("Select from the menu:")
                    .UseConverter(GetMenuOptions)
                    .AddChoices(Enum.GetValues<MenuOptions>())
            );

            switch (selection)
            {
                case MenuOptions.GetAll:
                    await _categoriesView.GetAllCategories();
                    break;
                case MenuOptions.Add:
                    await _categoriesView.CreateCategory();
                    break;
                case MenuOptions.Delete:
                    await _categoriesView.DeleteCategory();
                    break;
                case MenuOptions.Update:
                    await _categoriesView.UpdateCategory();
                    break;
                case MenuOptions.MainMenu:
                    exit = true;
                    break;
            }
        }
    }

    private static string GetMenuOptions(MenuOptions options)
    {
        return options switch
        {
            MenuOptions.GetAll => "Get All Categories",
            MenuOptions.Add => "Add Category",
            MenuOptions.Delete => "Delete Category",
            MenuOptions.Update => "Update Category",
            MenuOptions.MainMenu => "Main Menu",
            _ => options.ToString()
        };
    }
}
