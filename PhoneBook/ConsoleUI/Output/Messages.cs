using PhoneBook.Domain.Validation;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Output;

internal class Messages
{
    internal void ErrorMessage(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]ERROR:[/] {error.Description}");
        }
    }

    internal void AddCancelledMessage(string name, string addType)
    {
        AnsiConsole.MarkupLineInterpolated($"Cancelled adding [green]{name}[/] to {addType}.");
    }
    internal void AddSucessfulMessage(string name, string addType)
    {
        AnsiConsole.MarkupLineInterpolated($"Successfully added [green]{name}[/] to {addType}");
    }
    internal void DeleteCancelledMessage(string name, string addType)
    {
        AnsiConsole.MarkupLineInterpolated($"Cancelled deleting [green]{name}[/] from {addType}.");
    }
    internal void DeleteSucessfulMessage(string name, string addType)
    {
        AnsiConsole.MarkupLineInterpolated($"Successfully deleted [green]{name}[/] from {addType}");
    }
    internal void RenameCategorySuccessfulMessage(string originalName, string newName)
    {
        AnsiConsole.MarkupLineInterpolated($"Successfully changed [yellow]{originalName}[/] to [green]{newName}[/]");
    }
    internal void RenameCategorCancelledMessage(string originalName, string newName)
    {
        AnsiConsole.MarkupLineInterpolated($"Cancelled changing [yellow]{originalName}[/] to [green]{newName}[/]");
    }

    internal void EditContactCancelledMessage(string name)
    {
        AnsiConsole.MarkupLineInterpolated($"Cancelled editing [green]{name}[/]");
    }
    internal void EditContactSuccessuflMessage(string name)
    {
        AnsiConsole.MarkupLineInterpolated($"Successfully updated [green]{name}[/]");
    }
}
