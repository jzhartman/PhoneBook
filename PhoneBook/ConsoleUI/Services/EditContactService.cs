using PhoneBook.Application.DTOs;

namespace PhoneBook.ConsoleUI.Services;

internal class EditContactService
{
    public async Task RunAsync(ContactResponse originalContact)
    {
        // Could print new input menu to allow user to apply changes
        // Accepting saves all changes
        // Cool
        Console.WriteLine("Editing contact....");
        Console.ReadLine();
    }
}
