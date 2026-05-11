using PhoneBook.Application.Contacts.DTOs;
using PhoneBook.Application.GetById;
using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.ConsoleUI.Views;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Services;

internal class LookupContactMenuService
{
    private readonly ContactSelectionService _contactSelectionService;
    private readonly DeleteContactService _deleteContactService;
    private readonly GetContactByIdHandler _getContactByIdHandler;
    private readonly EditContactService _editContactService;
    private readonly ContactDetailsView _contactDetailsView;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public LookupContactMenuService(ContactSelectionService contactSelectionService, DeleteContactService deleteContactService,
                                    GetContactByIdHandler getContactByIdHandler,
                                    EditContactService editContactService, ContactDetailsView contactDetailsView,
                                    Messages messages, UserInput userInput)
    {
        _contactSelectionService = contactSelectionService;
        _deleteContactService = deleteContactService;
        _editContactService = editContactService;

        _getContactByIdHandler = getContactByIdHandler;

        _contactDetailsView = contactDetailsView;

        _messages = messages;
        _userInput = userInput;
    }

    internal async Task RunAsync()
    {
        bool returnToMainMenu = false;
        LookupMenuOptions[] menuOptions = Enum.GetValues<LookupMenuOptions>();

        Console.Clear();
        var contactResult = await _contactSelectionService.RunAsync();

        if (contactResult.IsFailure || contactResult.Value is null)
        {
            _messages.ErrorMessage(contactResult.Errors);
            _userInput.PressAnyKeyToContinue();
            return;
        }

        while (returnToMainMenu == false)
        {
            Console.Clear();

            AnsiConsole.WriteLine($"Viewing contact entry for {contactResult.Value.FirstName} {contactResult.Value.LastName}:");
            AnsiConsole.WriteLine();
            _contactDetailsView.Render(contactResult.Value);
            AnsiConsole.WriteLine();

            RenderContactDetailKeyOptions();

            var keyInfo = Console.ReadKey(true);
            var operation = await ManageKeyPressMenu(keyInfo, contactResult.Value);

            if (operation == LookupMenuOptions.Exit || operation == LookupMenuOptions.Delete) returnToMainMenu = true;
            if (operation == LookupMenuOptions.Update) contactResult = await _getContactByIdHandler.HandleAsync(contactResult.Value.ContactId);

            if (contactResult.IsFailure)
            {
                _messages.ErrorMessage(new[] { Errors.LoadEditDataFailed });
                _userInput.PressAnyKeyToContinue();
            }
        }
    }


    private void RenderContactDetailKeyOptions()
    {
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Spectre.Console.Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Key");
        table.AddColumn("Operation");

        table.AddRow("D", "Delete Contact");
        table.AddRow("E", "Edit Contact");
        table.AddRow("M", "Return to Main Menu");

        AnsiConsole.Write(table);
    }

    private async Task<LookupMenuOptions> ManageKeyPressMenu(ConsoleKeyInfo keyInfo, ContactResponse contact)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.D:
                await _deleteContactService.RunAsync(contact);
                return LookupMenuOptions.Delete;
            case ConsoleKey.E:
                await _editContactService.RunAsync(contact);
                return LookupMenuOptions.Update;
            case ConsoleKey.M:
                return LookupMenuOptions.Exit;
            default:
                _messages.ErrorMessage(new[] { new Error("Input", "Invalid key press") });
                Console.WriteLine("ERROR! Invalid key press");
                _userInput.PressAnyKeyToContinue();
                return LookupMenuOptions.Unknown;
        }
    }
}
