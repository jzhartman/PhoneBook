using PhoneBook.Application.DeleteContact;
using PhoneBook.Application.DTOs;
using PhoneBook.Application.SaveChanges;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.ConsoleUI.Services;

internal class DeleteContactService
{
    private readonly DeleteContactHandler _deleteContactHandler;
    private readonly SaveChangesHandler _saveChangesHandler;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public DeleteContactService(DeleteContactHandler deleteContactHandler, SaveChangesHandler saveChangesHandler, Messages messages, UserInput userInput)
    {
        _deleteContactHandler = deleteContactHandler;
        _saveChangesHandler = saveChangesHandler;
        _messages = messages;
        _userInput = userInput;
    }

    internal async Task RunAsync(ContactResponse contact)
    {
        var deleteResult = await _deleteContactHandler.HandleAsync(contact);
        var errors = new List<Error>();

        if (deleteResult.IsSuccess)
        {
            var saveResult = await _saveChangesHandler.HandleAsync();

            if (saveResult is null)
                errors.Add(Errors.SaveResponseNull);

            else if (saveResult.IsFailure)
                errors.AddRange(saveResult.Errors);

            else if (saveResult.IsSuccess)
                return;
        }

        if (deleteResult.IsFailure)
            errors.AddRange(deleteResult.Errors);

        if (deleteResult is null)
            errors.Add(Errors.GenericNull);

        _messages.ErrorMessage(errors);
        _userInput.PressAnyKeyToContinue();
    }
}
