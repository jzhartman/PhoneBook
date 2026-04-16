using PhoneBook.Application.DTOs;
using PhoneBook.Application.EditContact;
using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Views;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Services;

internal class EditContactService
{
    private readonly EditContactView _editContactView;
    private readonly UserInput _userInput;
    private readonly EditContactHandler _editContactHandler;

    public EditContactService(EditContactView editContactView, UserInput userInput, EditContactHandler editContactHandler)
    {
        _editContactView = editContactView;
        _userInput = userInput;
        _editContactHandler = editContactHandler;
    }

    /*
        TODO: Clean this garbage up!
        This is a bit messy
        Will need to clean this up a lot and work out a better flow...

        Current challenges:
            > How to store multiple changes to the ContactResponse object (it is a record)
                - Create ViewModel based on contact and pass that through for editing???
                - Updated _editContactView table parameter to ensure it is used -- also apply color for changed data? Include in VM?
            > How to save changes when ready
    */
    public async Task RunAsync(ContactResponse contact)
    {
        bool stillEditing = true;

        while (stillEditing)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[green]Editing[/] contact entry for {contact.FirstName} {contact.LastName}:");
            AnsiConsole.WriteLine();

            _editContactView.Render(contact);
            AnsiConsole.WriteLine();

            RenderEditContactKeyOptions();

            var keyInfo = Console.ReadKey(true);
            var options = await ManageKeyPressMenuFromCategory(keyInfo, contact);

            if (options == EditContactOptions.Save || options == EditContactOptions.Cancel) stillEditing = false;
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

    private async Task<EditContactOptions> ManageKeyPressMenuFromCategory(ConsoleKeyInfo keyInfo, ContactResponse contact)
    {
        // TODO: Inefficient mess to fix...

        switch (keyInfo.Key)
        {
            case ConsoleKey.F:
                _userInput.GetTextFromUser("Please enter the new first name:");
                return EditContactOptions.FirstName;
            case ConsoleKey.L:
                _userInput.GetTextFromUser("Please enter the new last name:");
                return EditContactOptions.LastName;
            case ConsoleKey.P:
                _userInput.GetPhoneNumberFromUser();
                return EditContactOptions.PhoneNumber;
            case ConsoleKey.E:
                _userInput.GetEmailAddressFromUser();
                return EditContactOptions.Email;
            case ConsoleKey.S:
                await _editContactHandler.HandleAsync(contact);
                return EditContactOptions.Save;
            case ConsoleKey.X:
                return EditContactOptions.Cancel;
            default:
                return EditContactOptions.Cancel;
        }
    }
}
