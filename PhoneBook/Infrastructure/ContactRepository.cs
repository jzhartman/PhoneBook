using Microsoft.EntityFrameworkCore;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.Infrastructure;

public class ContactRepository : IContactRepository
{
    private readonly PhoneBookDbContext _context;

    public ContactRepository(PhoneBookDbContext context)
    {
        _context = context;
    }


    public async Task<Result> AddAsync(Contact contact)
    {
        try
        {
            if (await ContactExists(contact))
                return Result.Failure(Errors.ContactExists);

            await _context.Contacts.AddAsync(contact);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("AddFailed", ex.Message));
        }
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

    private async Task<bool> ContactExists(Contact contact)
    {
        return await _context.Contacts.AnyAsync(c => c.FirstName == contact.FirstName && c.LastName == contact.LastName
                                                            && c.Email == contact.Email && c.PhoneNumber == contact.PhoneNumber);
    }
}
