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

    private readonly AddCategoryService _addCategoryService;
    private readonly LookupCategoryMenuService _lookupCategoryMenuService;

    public MainMenuService(MainMenuView mainMenu,
                            AddContactService addContactService, LookupContactMenuService lookupContactMenuService,
                            AddCategoryService addCategoryService, LookupCategoryMenuService lookupCategoryMenuService)
    {
        _mainMenu = mainMenu;

        _addContactService = addContactService;
        _lookupContactMenuService = lookupContactMenuService;

        _addCategoryService = addCategoryService;
        _lookupCategoryMenuService = lookupCategoryMenuService;
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
                case MainMenuOptions.ReadContacts:
                    await _lookupContactMenuService.RunAsync();
                    break;
                case MainMenuOptions.AddCategory:
                    await _addCategoryService.RunAsync();
                    break;
                case MainMenuOptions.ReadCategory:
                    await _lookupCategoryMenuService.RunAsync();
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
