using PhoneBook.Application.Categories.DTOs;
using PhoneBook.Application.Categories.GetCategoryById;
using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.ConsoleUI.Services.Categories;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Services;

internal class LookupCategoryMenuService
{
    private readonly CategorySelectionService _categorySelectionService;
    private readonly GetCategoryByIdHandler _getCategoryByIdHandler;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public LookupCategoryMenuService(CategorySelectionService categorySelectionService, GetCategoryByIdHandler getCategoryByIdHandler,
                                    Messages messages, UserInput userInput)
    {
        _categorySelectionService = categorySelectionService;
        _getCategoryByIdHandler = getCategoryByIdHandler;

        _messages = messages;
        _userInput = userInput;
    }

    internal async Task RunAsync()
    {
        bool returnToMainMenu = false;
        LookupMenuOptions[] menuOptions = Enum.GetValues<LookupMenuOptions>();

        Console.Clear();
        var categegoryResult = await _categorySelectionService.RunAsync();

        if (categegoryResult.IsFailure || categegoryResult.Value is null)
        {
            _messages.ErrorMessage(categegoryResult.Errors);
            _userInput.PressAnyKeyToContinue();
            return;
        }

        while (returnToMainMenu == false)
        {
            Console.Clear();

            AnsiConsole.WriteLine($"Viewing contacts in category {categegoryResult.Value.Name}:");
            AnsiConsole.WriteLine();
            //_contactDetailsView.Render(contactResult.Value);
            AnsiConsole.WriteLine();

            RenderContactDetailKeyOptions();

            var keyInfo = Console.ReadKey(true);
            var operation = await ManageKeyPressMenu(keyInfo, categegoryResult.Value);

            if (operation == LookupMenuOptions.Exit || operation == LookupMenuOptions.Delete) returnToMainMenu = true;
            if (operation == LookupMenuOptions.Update) categegoryResult = await _getCategoryByIdHandler.HandleAsync(categegoryResult.Value.Id);

            if (categegoryResult.IsFailure)
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

    private async Task<LookupMenuOptions> ManageKeyPressMenu(ConsoleKeyInfo keyInfo, CategoryResponse contact)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.D:
                //await _deleteContactService.RunAsync(contact);
                return LookupMenuOptions.Delete;
            case ConsoleKey.E:
                //await _editContactService.RunAsync(contact);
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
