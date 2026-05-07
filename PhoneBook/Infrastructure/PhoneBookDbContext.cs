using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Infrastructure;

public class PhoneBookDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    //public DbSet<ContactCategory> ContactCategories { get; set; }

    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<ContactCategory>().ToTable("ContactCategories");
        modelBuilder.Entity<Contact>().ToTable("Contacts");

        modelBuilder.Entity<Contact>()
            .HasData(new List<Contact>
            {
                new Contact
                {
                    Id = 1,
                    FirstName = "Malcolm",
                    LastName = "Reynolds",
                    PhoneNumber = "1111111111",
                    Email = "browncoat@serenity.com",
                    //CategoryId = 1
                },
                new Contact
                {
                    Id = 2,
                    FirstName = "Inara",
                    LastName = "Serra",
                    PhoneNumber = "2222222222",
                    Email = "companion@serenity.com",
                    //CategoryId = 1
                },
                new Contact
                {
                    Id = 3,
                    FirstName = "Shepherd",
                    LastName = "Book",
                    PhoneNumber = "3333333333",
                    Email = "book@serenity.com",
                    //CategoryId = 1
                },
                new Contact
                {
                    Id = 4,
                    FirstName = "Jayne",
                    LastName = "Cobb",
                    PhoneNumber = "4444444444",
                    Email = "vera@serenity.com",
                    //CategoryId = 1
                },
                new Contact
                {
                    Id = 5,
                    FirstName = "Kaylee",
                    LastName = "Frye",
                    PhoneNumber = "5555555555",
                    Email = "mechanic@serenity.com",
                    //CategoryId = 1
                },
                new Contact
                {
                    Id = 6,
                    FirstName = "Simon",
                    LastName = "Tam",
                    PhoneNumber = "6666666666",
                    Email = "awesomeDoctor@serenity.com",
                    //CategoryId = 1
                },
                new Contact
                {
                    Id = 7,
                    FirstName = "River",
                    LastName = "Tam",
                    PhoneNumber = "7777777777",
                    Email = "miranda@serenity.com",
                    //CategoryId = 1
                }
            });
    }
}
