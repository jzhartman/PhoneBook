using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Infrastructure;

internal class PhoneBookDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().ToTable("Contacts");
    }
}
