using PhoneBook.Application.Categories.DTOs;
using PhoneBook.Application.Contacts.DTOs;
using PhoneBook.Application.GetById;
using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.ConsoleUI.Services.Categories;
using PhoneBook.ConsoleUI.Views;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Services.Contacts;

internal class ViewContactService
{
    private readonly CategorySelectionService _categorySelectionService;
    private readonly ContactSelectionService _contactSelectionService;
    private readonly DeleteContactService _deleteContactService;
    private readonly GetContactByIdHandler _getContactByIdHandler;
    private readonly EditContactService _editContactService;
    private readonly ContactDetailsView _contactDetailsView;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public ViewContactService(ContactSelectionService contactSelectionService, DeleteContactService deleteContactService,
        CategorySelectionService categorySelectionService,
                                    GetContactByIdHandler getContactByIdHandler,
                                    EditContactService editContactService, ContactDetailsView contactDetailsView,
                                    Messages messages, UserInput userInput)
    {
        _categorySelectionService = categorySelectionService;
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
        ManageSubMenuOptions[] menuOptions = Enum.GetValues<ManageSubMenuOptions>();

        Console.Clear();
        var category = await _categorySelectionService.RunAsync(true);

        var contact = await _contactSelectionService.RunAsync(category ?? new(-1, "ALL"));

        if (contact is null)
            return;

        while (returnToMainMenu == false)
        {
            Console.Clear();

            AnsiConsole.WriteLine($"Viewing contact entry for {contact.FirstName} {contact.LastName}:");
            AnsiConsole.WriteLine();


            /*
             *  HERE IS WHERE WE LEFT OFF
             * 
             * Passing category to view is incorrect... this is assigned -1 and ALL is user selects all
             * These are not values that define the contact
             * Should create a contact view model and pass that in
             * ViewModel should be created in ContactSelectionService
             *      - Should include a call to a repo method to GetCategoryNameById(id)
             */





            _contactDetailsView.Render(contact, category);
            AnsiConsole.WriteLine();

            RenderContactDetailKeyOptions();

            var keyInfo = Console.ReadKey(true);
            var operation = await ManageKeyPressMenu(keyInfo, contact, category);

            if (operation == ManageSubMenuOptions.Exit || operation == ManageSubMenuOptions.Delete)
                returnToMainMenu = true;

            if (operation == ManageSubMenuOptions.Edit)
            {
                var updatedContact = await _getContactByIdHandler.HandleAsync(contact.ContactId);

                if (updatedContact.IsFailure)
                {
                    _messages.ErrorMessage(new[] { Errors.LoadEditDataFailed });
                    _userInput.PressAnyKeyToContinue();
                    return;
                }

                contact = updatedContact.Value;
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

    private async Task<ManageSubMenuOptions> ManageKeyPressMenu(ConsoleKeyInfo keyInfo, ContactResponse contact, CategoryResponse category)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.D:
                await _deleteContactService.RunAsync(contact);
                return ManageSubMenuOptions.Delete;
            case ConsoleKey.E:
                await _editContactService.RunAsync(contact, category);
                return ManageSubMenuOptions.Edit;
            case ConsoleKey.M:
                return ManageSubMenuOptions.Exit;
            default:
                _messages.ErrorMessage(new[] { new Error("Input", "Invalid key press") });
                Console.WriteLine("ERROR! Invalid key press");
                _userInput.PressAnyKeyToContinue();
                return ManageSubMenuOptions.Unknown;
        }
    }
}
