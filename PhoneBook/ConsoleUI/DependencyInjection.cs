using Microsoft.Extensions.DependencyInjection;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.ConsoleUI.Services;
using PhoneBook.ConsoleUI.Views;

namespace PhoneBook.ConsoleUI;

internal static class DependencyInjection
{
    internal static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<UserInput>();
        services.AddTransient<Messages>();

        services.AddTransient<MainMenuView>();
        services.AddTransient<ContactSelectionView>();
        services.AddTransient<ContactDetailsView>();
        services.AddTransient<EditContactView>();

        services.AddTransient<MainMenuService>();
        services.AddTransient<AddContactService>();
        services.AddTransient<LookupContactMenuService>();
        services.AddTransient<DeleteContactService>();
        services.AddTransient<ContactSelectionService>();
        services.AddTransient<EditContactService>();

        services.AddTransient<AddCategoryService>();
        services.AddTransient<LookupCategoryMenuService>();

        return services;
    }
}
