using PhoneBook.Application.DTOs;
using PhoneBook.Application.GetAllContacts;
using PhoneBook.ConsoleUI.Views;

namespace PhoneBook.ConsoleUI.Services;

internal class GetContactSelectionService
{
    private readonly GetAllContactsHandler _getAllContactsHandler;
    private readonly ContactSelectionView _contactSelectionView;

    public GetContactSelectionService(GetAllContactsHandler getAllContactsHandler, ContactSelectionView contactSelectionView)
    {
        _getAllContactsHandler = getAllContactsHandler;
        _contactSelectionView = contactSelectionView;
    }

    public async Task<ContactResponse> RunAsync()
    {
        var contacts = await _getAllContactsHandler.HandleAsync();

        return _contactSelectionView.Render(contacts.ToArray());
    }
}
