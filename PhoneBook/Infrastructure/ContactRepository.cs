using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Infrastructure;

public class ContactRepository : IContactRepository
{
    private readonly PhoneBookDbContext _context;

    public ContactRepository(PhoneBookDbContext context)
    {
        _context = context;
    }


    public Task AddAsync(Contact contact)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Contact contact)
    {
        throw new NotImplementedException();
    }

    public Task<List<Contact>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Contact> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Contact contact)
    {
        throw new NotImplementedException();
    }
}
