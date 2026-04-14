using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Application.AddContact;
using PhoneBook.Application.DeleteContact;
using PhoneBook.Application.GetAllContacts;
using PhoneBook.Application.SaveChanges;

namespace PhoneBook.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<GetAllContactsHandler>();
        services.AddTransient<AddContactHandler>();
        services.AddTransient<DeleteContactHandler>();
        services.AddTransient<SaveChangesHandler>();

        return services;
    }
}
