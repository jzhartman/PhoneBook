using PhoneBook.Application.DeleteContact;
using PhoneBook.Application.DTOs;
using PhoneBook.Application.SaveChanges;
using PhoneBook.ConsoleUI.Output;

namespace PhoneBook.ConsoleUI.Services;

internal class DeleteContactService
{
    private readonly DeleteContactHandler _deleteContactHandler;
    private readonly SaveChangesHandler _saveChangesHandler;
    private readonly Messages _messages;

    public DeleteContactService(DeleteContactHandler deleteContactHandler, SaveChangesHandler saveChangesHandler, Messages messages)
    {
        _deleteContactHandler = deleteContactHandler;
        _saveChangesHandler = saveChangesHandler;
        _messages = messages;
    }

    internal async Task RunAsync(ContactResponse contact)
    {
        var deleteResult = await _deleteContactHandler.HandleAsync(contact);

        if (deleteResult.IsFailure)
            _messages.ErrorMessage(deleteResult.Errors);

        if (deleteResult.IsSuccess)
            await _saveChangesHandler.HandleAsync();
    }
}
