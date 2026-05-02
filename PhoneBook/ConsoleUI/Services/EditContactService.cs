using PhoneBook.Application.DTOs;
using PhoneBook.Application.EditContact;
using PhoneBook.Application.SaveChanges;
using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Models;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.ConsoleUI.Views;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Services;

internal class EditContactService
{
    private readonly EditContactView _editContactView;
    private readonly UserInput _userInput;
    private readonly Messages _messages;
    private readonly EditContactHandler _editContactHandler;
    private readonly SaveChangesHandler _saveChangesHandler;

    public EditContactService(EditContactView editContactView, UserInput userInput, Messages messages
                                EditContactHandler editContactHandler, SaveChangesHandler saveChangesHandler)
    {
        _editContactView = editContactView;
        _userInput = userInput;
        _messages = messages;
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
            var exitCode = await ManageKeyPressMenuFromCategory(keyInfo, originalContact, contact);

            if (exitCode == EditContactExitCode.Save)
            {
                stillEditing = false;
                await UpdateContact(contact);
            }

            if (exitCode == EditContactExitCode.Cancel) stillEditing = false;
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
        var updateResult = await _editContactHandler.HandleAsync(new ContactResponse(contact.Id,
                                                                        contact.FirstName,
                                                                        contact.LastName,
                                                                        contact.PhoneNumber,
                                                                        contact.Email));

        var errors = new List<Error>();

        if (updateResult.IsSuccess)
        {
            var saveResult = await _saveChangesHandler.HandleAsync();

            if (saveResult is null)
                errors.Add(Errors.SaveResponseNull);

            else if (saveResult.IsFailure)
                errors.AddRange(saveResult.Errors);

            else if (saveResult.IsSuccess)
                return;
        }

        if (updateResult.IsFailure)
            errors.AddRange(updateResult.Errors);

        if (updateResult is null)
            errors.Add(Errors.GenericNull);

        _messages.ErrorMessage(errors);
        _userInput.PressAnyKeyToContinue();
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
