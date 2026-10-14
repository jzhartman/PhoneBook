using Microsoft.EntityFrameworkCore;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Infrastructure;
using PhoneBook.Infrastructure.Repositories;

namespace PhoneBook.IntegrationTests.Fixtures;

public class DatabaseFixture : IDisposable
{
    public string DbFile { get; }
    public PhoneBookDbContext Context { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IContactRepository ContactRepository { get; }

    public DatabaseFixture()
    {
        DbFile = Path.GetTempFileName();

        var options = new DbContextOptionsBuilder<PhoneBookDbContext>()
            .UseSqlite($"Data Source={DbFile}")
            .Options;

        Context = new PhoneBookDbContext(options, disableSeedData: true);
        Context.Database.EnsureCreated();

        CategoryRepository = new CategoryRepository(Context);
        ContactRepository = new ContactRepository(Context);
    }

    public void Dispose()
    {
        Context.Dispose();
        File.Delete(DbFile);
    }

    public void SeedCategory(ContactCategory category)
    {
        var input = new ContactCategory { Id = category.Id, Name = category.Name };
        Context.ContactCategories.Add(input);
        Context.SaveChanges();
    }

    public void SeedCategory(List<ContactCategory> categories)
    {
        foreach (var category in categories)
        {
            SeedCategory(category);
        }
    }

    public void SeedCategoryList()
    {
        var categories = new List<ContactCategory>
        {
            new ContactCategory {Name = "Family"},
            new ContactCategory {Name = "Friends"},
            new ContactCategory {Name = "Work"},
            new ContactCategory {Name = "School"}
        };

        Context.ContactCategories.AddRange(categories);
        Context.SaveChanges();
    }

    public Contact SeedContact(string firstName, string lastName, string phoneNumber, string email, int categoryId)
    {
        var contact = new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = email,
            CategoryId = categoryId
        };

        return SeedContact(contact);
    }

    public Contact SeedContact(Contact contact)
    {
        Context.Contacts.Add(contact);
        Context.SaveChanges();

        return contact;
    }

    public List<Contact> SeedContactList()
    {
        var category = new ContactCategory { Name = "Crew" };
        SeedCategory(category);

        var contacts = new List<Contact>
        {
            new Contact
            {
                FirstName = "Malcolm",
                LastName = "Reynolds",
                PhoneNumber = "1111111111",
                Email = "browncoat@serenity.com",
                CategoryId = category.Id
            },
            new Contact
            {
                FirstName = "Inara",
                LastName = "Serra",
                PhoneNumber = "2222222222",
                Email = "companion@serenity.com",
                CategoryId = category.Id
            },
            new Contact
            {
                FirstName = "Shepherd",
                LastName = "Book",
                PhoneNumber = "3333333333",
                Email = "book@serenity.com",
                CategoryId = category.Id
            },
            new Contact
            {
                FirstName = "Jayne",
                LastName = "Cobb",
                PhoneNumber = "4444444444",
                Email = "vera@serenity.com",
                CategoryId = category.Id
            },
            new Contact
            {
                FirstName = "Kaylee",
                LastName = "Frye",
                PhoneNumber = "5555555555",
                Email = "mechanic@serenity.com",
                CategoryId = category.Id
            },
            new Contact
            {
                FirstName = "Simon",
                LastName = "Tam",
                PhoneNumber = "6666666666",
                Email = "awesomeDoctor@serenity.com",
                CategoryId = category.Id
            },
            new Contact
            {
                FirstName = "River",
                LastName = "Tam",
                PhoneNumber = "7777777777",
                Email = "miranda@serenity.com",
                CategoryId = category.Id
            },
            new Contact
            {
                FirstName = "Hoban",
                LastName = "Washburne",
                PhoneNumber = "8888888",
                Email = "leafOnTheWind@serenity.com",
                CategoryId = category.Id
            },
            new Contact
            {
                FirstName = "Zoe",
                LastName = "Washburne",
                PhoneNumber = "99999999",
                Email = "browncoats@serenity.com",
                CategoryId = category.Id
            }
        };

        Context.Contacts.AddRange(contacts);
        Context.SaveChanges();

        return contacts;
    }

    public void ResetDatabase()
    {
        Context.ContactCategories.RemoveRange(Context.ContactCategories);
        Context.Contacts.RemoveRange(Context.Contacts);
        Context.SaveChanges();
    }

}
