using Microsoft.EntityFrameworkCore;
using PhoneBook.Application.Interfaces;
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

        Context = new PhoneBookDbContext(options);
        Context.Database.EnsureCreated();

        CategoryRepository = new CategoryRepository(Context);
        ContactRepository = new ContactRepository(Context);
    }

    public void Dispose()
    {
        Context.Dispose();
        File.Delete(DbFile);
    }
}
