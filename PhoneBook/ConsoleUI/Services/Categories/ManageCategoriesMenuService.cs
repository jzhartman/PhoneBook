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

internal class ManageCategoriesMenuService
{
    private readonly CategorySelectionService _categorySelectionService;
    private readonly AddCategoryService _addCategoryService;
    private readonly GetCategoryByIdHandler _getCategoryByIdHandler;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public ManageCategoriesMenuService(CategorySelectionService categorySelectionService, GetCategoryByIdHandler getCategoryByIdHandler,
                                    AddCategoryService addCategoryService,
                                    Messages messages, UserInput userInput)
    {
        _categorySelectionService = categorySelectionService;
        _getCategoryByIdHandler = getCategoryByIdHandler;

        _addCategoryService = addCategoryService;

        _messages = messages;
        _userInput = userInput;
    }

    internal async Task RunAsync()
    {
        bool returnToMainMenu = false;
        ManageSubMenuOptions[] menuOptions = Enum.GetValues<ManageSubMenuOptions>();

        Console.Clear();
        var category = await _categorySelectionService.RunAsync();

        while (returnToMainMenu == false)
        {
            Console.Clear();

            AnsiConsole.WriteLine($"Managing category: {category?.Name ?? "UNKNOWN"}:");
            AnsiConsole.WriteLine();

            RenderManageCategoriesKeyOptions();

            var keyInfo = Console.ReadKey(true);
            var operation = await ManageKeyPressMenu(keyInfo, category);

            if (operation == ManageSubMenuOptions.Exit || operation == ManageSubMenuOptions.Delete)
                returnToMainMenu = true;

            if (operation == ManageSubMenuOptions.Edit)
            {
                var updatedCategory = await _getCategoryByIdHandler.HandleAsync(category.Id);

                if (updatedCategory.IsFailure)
                {
                    _messages.ErrorMessage(new[] { Errors.LoadEditDataFailed });
                    _userInput.PressAnyKeyToContinue();
                    return;
                }

                category = updatedCategory.Value;
            }
        }
    }


    private void RenderManageCategoriesKeyOptions()
    {
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Spectre.Console.Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Key");
        table.AddColumn("Operation");

        table.AddRow("D", "Delete Category");
        table.AddRow("R", "Rename Category");
        table.AddRow("X", "Return to Main Menu");

        AnsiConsole.Write(table);
    }

    private async Task<ManageSubMenuOptions> ManageKeyPressMenu(ConsoleKeyInfo keyInfo, CategoryResponse category)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.D:
                //await _editContactService.RunAsync(contact);
                Console.WriteLine("Delete this someday");
                _userInput.PressAnyKeyToContinue();
                return ManageSubMenuOptions.Delete;
            case ConsoleKey.R:
                //await _editContactService.RunAsync(contact);
                Console.WriteLine("Rename this someday");
                _userInput.PressAnyKeyToContinue();
                return ManageSubMenuOptions.Edit;
            case ConsoleKey.X:
                return ManageSubMenuOptions.Exit;
            default:
                _messages.ErrorMessage(new[] { new Error("Input", "Invalid key press") });
                Console.WriteLine("ERROR! Invalid key press");
                _userInput.PressAnyKeyToContinue();
                return ManageSubMenuOptions.Unknown;
        }
    }
}
