using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Views;

namespace PhoneBook.ConsoleUI.Services;

internal class MainMenuService
{
    private readonly MainMenuView _mainMenu;

    private readonly AddContactService _addContactService;
    private readonly LookupContactMenuService _lookupContactMenuService;

    public MainMenuService(MainMenuView mainMenu, AddContactService addContactService,
                    LookupContactMenuService lookupContactMenuService)
    {
        _mainMenu = mainMenu;

        _addContactService = addContactService;
        _lookupContactMenuService = lookupContactMenuService;
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
                case MainMenuOptions.Add:
                    await _addContactService.RunAsync();
                    break;
                case MainMenuOptions.Read:
                    await _lookupContactMenuService.RunAsync();
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
