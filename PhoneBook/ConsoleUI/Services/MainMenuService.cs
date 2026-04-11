using PhoneBook.ConsoleUI.Enums;
using PhoneBook.ConsoleUI.Views;

namespace PhoneBook.ConsoleUI.Services;

internal class MainMenuService
{
    private readonly MainMenuView _mainMenu;

    public MainMenuService(MainMenuView mainMenu)
    {
        _mainMenu = mainMenu;
    }

    internal void Run()
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
                    Console.WriteLine("Adding contact...");
                    Console.ReadLine();
                    break;
                case MainMenuOptions.Delete:
                    Console.WriteLine("Deleting contact...");
                    Console.ReadLine();
                    break;
                case MainMenuOptions.Update:
                    Console.WriteLine("Editing contact...");
                    Console.ReadLine();
                    break;
                case MainMenuOptions.Read:
                    Console.WriteLine("Reading contact...");
                    Console.ReadLine();
                    break;
                case MainMenuOptions.ReadAll:
                    Console.WriteLine("Reading all contacts...");
                    Console.ReadLine();
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
