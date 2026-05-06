using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Infrastructure;

public class PhoneBookDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactCategory> ContactCategories { get; set; }

    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactCategory>().ToTable("ContactCategories");
        modelBuilder.Entity<Contact>().ToTable("Contacts");

        //modelBuilder.Entity<Contact>()
        //    .HasData(new List<Contact>
        //    {
        //        new Contact
        //        {
        //            ContactId = 1,
        //            FirstName = "Malcolm",
        //            LastName = "Reynolds",
        //            PhoneNumber = "1111111111",
        //            Email = "mal@serenity.com",
        //            CategoryId = 1
        //        },
        //        new Contact
        //        {
        //            ContactId = 2,
        //            FirstName = "Inara",
        //            LastName = "Serra",
        //            PhoneNumber = "2222222222",
        //            Email = "inara@serenity.com",
        //            CategoryId = 1
        //        },
        //        new Contact
        //        {
        //            ContactId = 3,
        //            FirstName = "Shepherd",
        //            LastName = "Book",
        //            PhoneNumber = "3333333333",
        //            Email = "book@serenity.com",
        //            CategoryId = 1
        //        },
        //        new Contact
        //        {
        //            ContactId = 4,
        //            FirstName = "Jayne",
        //            LastName = "Cobb",
        //            PhoneNumber = "4444444444",
        //            Email = "vera@serenity.com",
        //            CategoryId = 1
        //        }
        //    });
    }
}
