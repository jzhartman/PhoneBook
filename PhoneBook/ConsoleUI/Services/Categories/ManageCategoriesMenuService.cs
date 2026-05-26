using PhoneBook.Application.Categories.GetCategoryById;
using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.ConsoleUI.Services.Categories;
using PhoneBook.ConsoleUI.Views;

namespace PhoneBook.ConsoleUI.Services;

internal class ManageCategoriesMenuService
{
    private readonly ManageCategoriesMenuView _manageCategoriesMenuView;
    private readonly DeleteCategoryService _deleteCategoryService;
    private readonly CategorySelectionService _categorySelectionService;
    private readonly AddCategoryService _addCategoryService;
    private readonly GetCategoryByIdHandler _getCategoryByIdHandler;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public ManageCategoriesMenuService(ManageCategoriesMenuView manageCategoriesMenuView, DeleteCategoryService deleteCategoryService,
                                    CategorySelectionService categorySelectionService, GetCategoryByIdHandler getCategoryByIdHandler,
                                    AddCategoryService addCategoryService,
                                    Messages messages, UserInput userInput)
    {
        _manageCategoriesMenuView = manageCategoriesMenuView;

        _deleteCategoryService = deleteCategoryService;
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

        while (returnToMainMenu == false)
        {
            Console.Clear();
            var selection = _manageCategoriesMenuView.Render(menuOptions);

            switch (selection)
            {
                case ManageSubMenuOptions.Add:
                    await _addCategoryService.RunAsync();
                    break;
                case ManageSubMenuOptions.Delete:
                    Console.WriteLine("Deleting category...");
                    _userInput.PressAnyKeyToContinue();
                    await _deleteCategoryService.RunAsync();
                    break;
                case ManageSubMenuOptions.Edit:
                    Console.WriteLine("Editing category...");
                    _userInput.PressAnyKeyToContinue();
                    //await _manageCategoriesMenuService.RunAsync();
                    break;
                case ManageSubMenuOptions.Exit:
                    returnToMainMenu = true;
                    break;
                default:
                    Console.WriteLine("INVALID MENU SELECTION");
                    _userInput.PressAnyKeyToContinue();
                    break;
            }
        }
    }
}
