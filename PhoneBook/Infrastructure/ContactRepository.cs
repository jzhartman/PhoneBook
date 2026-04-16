using Microsoft.EntityFrameworkCore;
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


    public async Task AddAsync(Contact contact)
    {
        // Validate for if it exists
        await _context.Contacts.AddAsync(contact);
    }

    public async Task DeleteAsync(Contact contact)
    {
        // Validate for if it exists
        await _context.Contacts
                    .Where(c => c.ContactId == contact.ContactId)
                    .ExecuteDeleteAsync();
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        return await _context.Contacts.AsNoTracking().ToListAsync();
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(c => c.ContactId == id);
    }

    public async Task UpdateAsync(Contact contact)
    {
        //_context.Update(contact);

        await _context.Contacts
                    .Where(c => c.ContactId == contact.ContactId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(c => c.FirstName, contact.FirstName)
                        .SetProperty(c => c.LastName, contact.LastName)
                        .SetProperty(c => c.Email, contact.Email)
                        .SetProperty(c => c.PhoneNumber, contact.PhoneNumber)
                    );
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
