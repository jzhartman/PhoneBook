using PhoneBook.Application.DTOs;
using PhoneBook.Application.GetById;
using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Views;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Services;

internal class LookupContactMenuService
{
    private readonly ContactSelectionService _contactSelectionService;
    private readonly DeleteContactService _deleteContactService;
    private readonly GetContactByIdHandler _getContactByIdHandler;
    private readonly EditContactService _editContactService;
    private readonly ContactDetailsView _contactDetailsView;

    public LookupContactMenuService(ContactSelectionService contactSelectionService, DeleteContactService deleteContactService,
                                    GetContactByIdHandler getContactByIdHandler,
                                    EditContactService editContactService, ContactDetailsView contactDetailsView)
    {
        _contactSelectionService = contactSelectionService;
        _deleteContactService = deleteContactService;
        _editContactService = editContactService;

        _getContactByIdHandler = getContactByIdHandler;

        _contactDetailsView = contactDetailsView;
    }

    internal async Task RunAsync()
    {
        bool returnToMainMenu = false;
        LookupMenuOptions[] menuOptions = Enum.GetValues<LookupMenuOptions>();

        Console.Clear();
        var contact = await _contactSelectionService.RunAsync();

        while (returnToMainMenu == false)
        {
            Console.Clear();

            AnsiConsole.WriteLine($"Viewing contact entry for {contact.FirstName} {contact.LastName}:");
            AnsiConsole.WriteLine();
            _contactDetailsView.Render(contact);
            AnsiConsole.WriteLine();

            RenderContactDetailKeyOptions();

            var keyInfo = Console.ReadKey(true);
            var operation = await ManageKeyPressMenu(keyInfo, contact);

            if (operation == LookupMenuOptions.Exit || operation == LookupMenuOptions.Delete) returnToMainMenu = true;
            if (operation == LookupMenuOptions.Update) await _getContactByIdHandler.HandleAsync(contact.ContactId);
        }

        Console.Write("Press any key to continue...");
        Console.ReadLine();
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
                Console.WriteLine("ERROR! Invalid key press");
                return LookupMenuOptions.Unknown;
        }
    }
}
