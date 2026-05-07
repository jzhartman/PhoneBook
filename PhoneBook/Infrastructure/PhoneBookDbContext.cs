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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhoneBookDbContext).Assembly);
    }
}
