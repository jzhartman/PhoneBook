using Microsoft.Extensions.DependencyInjection;
using PhoneBook.ConsoleUI.Services;
using PhoneBook.ConsoleUI.Views;

namespace PhoneBook.ConsoleUI;

internal static class DependencyInjection
{
    internal static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<MainMenuView>();
        services.AddTransient<ContactSelectionView>();

        services.AddTransient<MainMenuService>();
        services.AddTransient<AddContactService>();
        services.AddTransient<DeleteContactService>();
        services.AddTransient<ContactSelectionService>();

        return services;
    }
}
