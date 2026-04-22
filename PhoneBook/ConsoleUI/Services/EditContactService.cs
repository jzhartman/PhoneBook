using PhoneBook.Application.DTOs;
using PhoneBook.Application.EditContact;
using PhoneBook.Application.SaveChanges;
using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Models;
using PhoneBook.ConsoleUI.Views;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Services;

internal class EditContactService
{
    private readonly EditContactView _editContactView;
    private readonly UserInput _userInput;
    private readonly EditContactHandler _editContactHandler;
    private readonly SaveChangesHandler _saveChangesHandler;

    public EditContactService(EditContactView editContactView, UserInput userInput, EditContactHandler editContactHandler,
                                SaveChangesHandler saveChangesHandler)
    {
        _editContactView = editContactView;
        _userInput = userInput;
        _editContactHandler = editContactHandler;
        _saveChangesHandler = saveChangesHandler;
    }

    public async Task RunAsync(ContactResponse originalContact)
    {
        bool stillEditing = true;
        var contact = new EditContactViewModel(originalContact);

        while (stillEditing)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[green]Editing[/] contact entry for {contact.FirstName} {contact.LastName}:");
            AnsiConsole.WriteLine();

            _editContactView.Render(contact);
            AnsiConsole.WriteLine();

            RenderEditContactKeyOptions();

            var keyInfo = Console.ReadKey(true);
            var options = await ManageKeyPressMenuFromCategory(keyInfo, originalContact, contact);

            if (options == EditContactExitCode.Save)
            {
                stillEditing = false;
                await UpdateContact(contact);
            }

            if (options == EditContactExitCode.Cancel) stillEditing = false;
        }
    }

    private void RenderEditContactKeyOptions()
    {
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Spectre.Console.Color.Blue)
                        .Title("Select an field to edit")
                        .ShowRowSeparators();

        table.AddColumn("Key");
        table.AddColumn("Operation");

        table.AddRow("F", "First Name");
        table.AddRow("L", "Last Name");
        table.AddRow("P", "Phone Number");
        table.AddRow("E", "Email");
        table.AddRow("S", "Save Changes");
        table.AddRow("X", "Cancel");

        AnsiConsole.Write(table);
    }

    private async Task<EditContactExitCode> ManageKeyPressMenuFromCategory(ConsoleKeyInfo keyInfo,
                                                                            ContactResponse originalContact,
                                                                            EditContactViewModel contact)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.F:
                ManageUpdateFirstName(originalContact, contact);
                break;
            case ConsoleKey.L:
                ManageUpdateLastName(originalContact, contact);
                break;
            case ConsoleKey.P:
                ManageUpdatePhoneNumber(originalContact, contact);
                break;
            case ConsoleKey.E:
                ManageUpdateEmail(originalContact, contact);
                break;
            case ConsoleKey.S:
                return EditContactExitCode.Save;
            case ConsoleKey.X:
                return EditContactExitCode.Cancel;
            default:
                return EditContactExitCode.Cancel;
        }
        return EditContactExitCode.None;
    }

    private async Task UpdateContact(EditContactViewModel contact)
    {
        await _editContactHandler.HandleAsync(new ContactResponse(contact.Id,
                                                            contact.FirstName,
                                                            contact.LastName,
                                                            contact.PhoneNumber,
                                                            contact.Email));
        await _saveChangesHandler.HandleAsync();
    }

    private void ManageUpdateFirstName(ContactResponse originalContact, EditContactViewModel contact)
    {
        contact.FirstName = _userInput.GetNameFromUser("Please enter the new first name:");
        contact.ChangedFirstName = (contact.FirstName == originalContact.FirstName) ? false : true;
    }
    private void ManageUpdateLastName(ContactResponse originalContact, EditContactViewModel contact)
    {
        contact.LastName = _userInput.GetNameFromUser("Please enter the new last name:");
        contact.ChangedLastName = (contact.LastName == originalContact.LastName) ? false : true;
    }

    private void ManageUpdatePhoneNumber(ContactResponse originalContact, EditContactViewModel contact)
    {
        contact.PhoneNumber = _userInput.GetPhoneNumberFromUser();
        contact.ChangedPhoneNumber = (contact.PhoneNumber == originalContact.PhoneNumber) ? false : true;
    }

    private void ManageUpdateEmail(ContactResponse originalContact, EditContactViewModel contact)
    {
        contact.Email = _userInput.GetEmailAddressFromUser();
        contact.ChangedEmail = (contact.Email == originalContact.Email) ? false : true;
    }
}
