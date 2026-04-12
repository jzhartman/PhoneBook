using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Infrastructure;

public class PhoneBookDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().ToTable("Contacts");

        modelBuilder.Entity<Contact>()
            .HasData(new List<Contact>
            {
                new Contact
                {
                    ContactId = 1,
                    FirstName = "Malcolm",
                    LastName = "Reynolds",
                    Email = "mal@serenity.com",
                    PhoneNumber = "1111111111"
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Inara",
                    LastName = "Serra",
                    Email = "inara@serenity.com",
                    PhoneNumber = "2222222222"
                },
                new Contact
                {
                    ContactId = 3,
                    FirstName = "Shepherd",
                    LastName = "Book",
                    Email = "book@serenity.com",
                    PhoneNumber = "3333333333"
                },
                new Contact
                {
                    ContactId = 4,
                    FirstName = "Jayne",
                    LastName = "Cobb",
                    Email = "vera@serenity.com",
                    PhoneNumber = "4444444444"
                }
            });
    }
}
