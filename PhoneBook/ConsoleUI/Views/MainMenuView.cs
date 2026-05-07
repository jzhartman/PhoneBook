using PhoneBook.ConsoleUI.Enums;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Views;

internal class MainMenuView
{
    internal MainMenuOptions Render(MainMenuOptions[] menuOptions)
    {
        Console.Clear();
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuOptions>()
            .Title("Select a menu option:")
            .UseConverter(m => m switch
            {
                MainMenuOptions.AddContact => "Add Contact",
                MainMenuOptions.ReadContacts => "View All Contacts",
                MainMenuOptions.AddCategory => "Add Category",
                MainMenuOptions.ReadCategory => "View Categories",
                MainMenuOptions.Exit => "Exit Application",
                _ => m.ToString()
            })
            .AddChoices(menuOptions));

        return selection;
    }
}
