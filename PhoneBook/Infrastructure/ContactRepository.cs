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
            return Result.Failure(new Error("Add Failed", ex.Message));
        }
    }

    public async Task<Result> DeleteAsync(Contact contact)
    {
        try
        {
            if (!(await ContactExists(contact)))
                return Result.Failure(Errors.ContactDoesNotExist);

            await _context.Contacts
                .Where(c => c.ContactId == contact.ContactId)
                .ExecuteDeleteAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {

            return Result.Failure(new Error("Delete Failed", ex.Message));
        }
    }

    public async Task<Result<List<Contact>>> GetAllAsync()
    {
        try
        {
            var contacts = await _context.Contacts.AsNoTracking().ToListAsync();

            if (contacts is null)
                contacts = new List<Contact>();

            return Result<List<Contact>>.Success(contacts);
        }
        catch (Exception ex)
        {
            return Result<List<Contact>>.Failure(new Error("Retreival Failed", ex.Message));
        }
    }

    public async Task<Result<Contact>> GetByIdAsync(int id)
    {
        try
        {
            var contact = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(c => c.ContactId == id);

            if (contact is null)
                contact = new Contact();

            return Result<Contact>.Success(contact);
        }
        catch (Exception ex)
        {
            return Result<Contact>.Failure(new Error("Retreival Failed", ex.Message));
        }
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
    public async Task<Result> SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Save Failed", ex.Message));
        }
    }

    private async Task<bool> ContactExists(Contact contact)
    {
        return await _context.Contacts.AnyAsync(c => c.FirstName == contact.FirstName && c.LastName == contact.LastName
                                                            && c.Email == contact.Email && c.PhoneNumber == contact.PhoneNumber);
    }
}
