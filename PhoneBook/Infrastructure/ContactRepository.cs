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
        _context.Contacts.Remove(contact);
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        return await _context.Contacts.ToListAsync();
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public async Task UpdateAsync(Contact contact)
    {
        _context.Update(contact);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
