using PhoneBook.Application.Categories.DeleteCategory;
using PhoneBook.Application.Contacts.SaveChanges;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.ConsoleUI.Services.Categories;

internal class DeleteCategoryService
{
    private readonly DeleteCategoryByIdHandler _deleteCategoryByIdHandler;
    private readonly CategorySelectionService _categorySelectionService;
    private readonly SaveChangesHandler _saveChangesHandler;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public DeleteCategoryService(DeleteCategoryByIdHandler deleteCategoryByIdHandler, CategorySelectionService categorySelectionService,
                                    SaveChangesHandler saveChangesHandler, Messages messages, UserInput userInput)
    {
        _deleteCategoryByIdHandler = deleteCategoryByIdHandler;
        _categorySelectionService = categorySelectionService;
        _saveChangesHandler = saveChangesHandler;
        _messages = messages;
        _userInput = userInput;
    }

    internal async Task RunAsync()
    {
        var category = await _categorySelectionService.RunAsync();

        var confirmDelete = _userInput.GetDeleteConfirmationFromUser(category.Name, "category list");

        if (confirmDelete)
        {
            var deleteResult = await _deleteCategoryByIdHandler.HandleAsync(category);

            var errors = new List<Error>();

            if (deleteResult.IsSuccess)
            {
                var saveResult = await _saveChangesHandler.HandleAsync();

                if (saveResult is null)
                    errors.Add(Errors.SaveResponseNull);

                else if (saveResult.IsFailure)
                    errors.AddRange(saveResult.Errors);

                else if (saveResult.IsSuccess)
                {
                    _messages.DeleteSucessfulMessage(category.Name, "category list");
                    _userInput.PressAnyKeyToContinue();
                    return;
                }
            }

            if (deleteResult.IsFailure)
                errors.AddRange(deleteResult.Errors);

            if (deleteResult is null)
                errors.Add(Errors.DeleteResponseNull);

            _messages.ErrorMessage(errors);
        }
        else
        {
            _messages.DeleteCancelledMessage(category.Name, "category list");
        }
        _userInput.PressAnyKeyToContinue();
    }
}
