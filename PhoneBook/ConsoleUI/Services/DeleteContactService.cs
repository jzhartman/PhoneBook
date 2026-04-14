using PhoneBook.Application.DeleteContact;
using PhoneBook.Application.SaveChanges;

namespace PhoneBook.ConsoleUI.Services;

internal class DeleteContactService
{
    private readonly ContactSelectionService _getContactSelectionService;
    private readonly DeleteContactHandler _deleteContactHandler;
    private readonly SaveChangesHandler _saveChangesHandler;

    public DeleteContactService(ContactSelectionService getContactSelectionService, DeleteContactHandler deleteContactHandler,
                                SaveChangesHandler saveChangesHandler)
    {
        _getContactSelectionService = getContactSelectionService;

        _deleteContactHandler = deleteContactHandler;
        _saveChangesHandler = saveChangesHandler;
    }

    internal async Task RunAsync()
    {
        var contact = await _getContactSelectionService.RunAsync();

        await _deleteContactHandler.HandleAsync(contact);
        await _saveChangesHandler.HandleAsync();


        Console.Write("Press any key to continue...");
        Console.ReadLine();
    }


}
