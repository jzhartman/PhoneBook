using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Services.Categories;
using PhoneBook.ConsoleUI.Services.Contacts;
using PhoneBook.ConsoleUI.Views;

namespace PhoneBook.ConsoleUI.Services;

internal class MainMenuService
{
    private readonly MainMenuView _mainMenu;

    private readonly AddContactService _addContactService;
    private readonly LookupContactMenuService _lookupContactMenuService;
    private readonly ManageCategoriesMenuService _manageCategoriesMenuService;

    private readonly AddCategoryService _addCategoryService;

    public MainMenuService(MainMenuView mainMenu,
                            AddContactService addContactService, LookupContactMenuService lookupContactMenuService,
                            ManageCategoriesMenuService manageCategoriesMenuService, AddCategoryService addCategoryService)
    {
        _mainMenu = mainMenu;

        _addContactService = addContactService;
        _lookupContactMenuService = lookupContactMenuService;
        _manageCategoriesMenuService = manageCategoriesMenuService;

        _addCategoryService = addCategoryService;
    }

    internal async Task RunAsync()
    {
        bool exitApp = false;
        MainMenuOptions[] menuOptions = Enum.GetValues<MainMenuOptions>();

        while (exitApp == false)
        {
            Console.Clear();
            var selection = _mainMenu.Render(menuOptions);

            switch (selection)
            {
                case MainMenuOptions.AddContact:
                    await _addContactService.RunAsync();
                    break;
                case MainMenuOptions.ViewContacts:              // ToDo: Refactor this to force selection by category -- Will need an ALL option
                    await _lookupContactMenuService.RunAsync();
                    break;
                case MainMenuOptions.AddCategory:               // ToDo: Would be nicer if this existed inside ManageCategories option
                    await _addCategoryService.RunAsync();
                    break;
                case MainMenuOptions.ManageCategories:
                    await _manageCategoriesMenuService.RunAsync();
                    break;
                case MainMenuOptions.Exit:
                    Console.WriteLine("Goodbye...");
                    Console.ReadLine();
                    exitApp = true;
                    break;
                default:
                    Console.WriteLine("ERROR!!!! Self destructing...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
