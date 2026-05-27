using PhoneBook.Application.Categories.UpdateCategory;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.ConsoleUI.Services.Categories;

internal class EditCategoryService
{
    private readonly CategorySelectionService _categorySelectionService;
    private readonly UpdateCategoryNameHandler _updateCategoryNameHandler;
    private readonly UserInput _userInput;
    private readonly Messages _messages;
    public EditCategoryService(CategorySelectionService categorySelectionService, UpdateCategoryNameHandler updateCategoryNameHandler,
        UserInput userInput, Messages messages)
    {
        _categorySelectionService = categorySelectionService;
        _updateCategoryNameHandler = updateCategoryNameHandler;
        _userInput = userInput;
        _messages = messages;
    }

    internal async Task RunAsync()
    {
        Console.Clear();
        var category = await _categorySelectionService.RunAsync();

        if (category is null)
        {
            _messages.ErrorMessage(new[] { Errors.GetResponseNull });
            _userInput.PressAnyKeyToContinue();
            return; ;
        }

        var newName = _userInput.GetNameFromUser($"Enter new name for category [green]{category.Name}[/]:");

        var result = await _updateCategoryNameHandler.HandleAsync(new(category.Id, category.Name, newName));

        if (result.IsFailure)
        {
            _messages.ErrorMessage(result.Errors);
            _userInput.PressAnyKeyToContinue();
            return; ;
        }
        else
        {
            Console.WriteLine($"Successefully updated {category.Name} to {newName}!");
            _userInput.PressAnyKeyToContinue();
        }
    }
}