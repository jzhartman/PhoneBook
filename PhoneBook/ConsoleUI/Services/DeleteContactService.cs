using PhoneBook.Application.DeleteContact;
using PhoneBook.Application.DTOs;
using PhoneBook.Application.SaveChanges;

namespace PhoneBook.ConsoleUI.Services;

internal class DeleteContactService
{
    private readonly DeleteContactHandler _deleteContactHandler;
    private readonly SaveChangesHandler _saveChangesHandler;

    public DeleteContactService(DeleteContactHandler deleteContactHandler, SaveChangesHandler saveChangesHandler)
    {
        _deleteContactHandler = deleteContactHandler;
        _saveChangesHandler = saveChangesHandler;
    }

    internal async Task RunAsync(ContactResponse contact)
    {
        await _deleteContactHandler.HandleAsync(contact);
        await _saveChangesHandler.HandleAsync();
    }
}
