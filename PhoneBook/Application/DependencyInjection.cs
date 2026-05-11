using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Application.Categories.AddCategory;
using PhoneBook.Application.Contacts.AddContact;
using PhoneBook.Application.Contacts.DeleteContact;
using PhoneBook.Application.Contacts.EditContact;
using PhoneBook.Application.Contacts.GetAllContacts;
using PhoneBook.Application.Contacts.SaveChanges;
using PhoneBook.Application.GetById;

namespace PhoneBook.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<GetAllContactsHandler>();
        services.AddTransient<GetContactByIdHandler>();
        services.AddTransient<AddContactHandler>();
        services.AddTransient<DeleteContactHandler>();
        services.AddTransient<SaveChangesHandler>();
        services.AddTransient<EditContactHandler>();

        services.AddTransient<AddCategoryHandler>();

        return services;
    }
}
