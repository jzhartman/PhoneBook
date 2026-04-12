using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Application.AddContact;
using PhoneBook.Application.SaveChanges;

namespace PhoneBook.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<AddContactHandler>();
        services.AddTransient<SaveChangesHandler>();

        return services;
    }
}
