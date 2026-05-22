using PhoneBook.Application.Categories.DTOs;
using PhoneBook.Application.Categories.GetAllCategories;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.ConsoleUI.Views;

namespace PhoneBook.ConsoleUI.Services.Categories;

internal class CategorySelectionService
{
    private readonly GetAllCategoriesHandler _getAllCategoriesHandler;
    private readonly CategorySelectionView _categorySelectionView;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public CategorySelectionService(GetAllCategoriesHandler getAllCategoriesHandler, CategorySelectionView categorySelectionView,
                                    Messages messages, UserInput userInput)
    {
        _getAllCategoriesHandler = getAllCategoriesHandler;
        _categorySelectionView = categorySelectionView;
        _messages = messages;
        _userInput = userInput;
    }

    internal async Task<CategoryResponse?> RunAsync(bool allowAll = false, bool allowAdd = false)
    {
        var result = await _getAllCategoriesHandler.HandleAsync();

        if (result.IsFailure || result.Value is null)
        {
            _messages.ErrorMessage(result.Errors);
            _userInput.PressAnyKeyToContinue();
            return null;
        }

        if (allowAll)
            result.Value.Prepend(new(-1, "All"));       //ToDo: Address this warning -- may cause some issues

        if (allowAdd)
            result.Value.Prepend(new(-1, "Add New Category"));

        return _categorySelectionView.Render(result.Value);
    }
}
