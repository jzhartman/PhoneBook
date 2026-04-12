using PhoneBook.Application.Interfaces;

namespace PhoneBook.Application.SaveChanges;

internal class SaveChangesHandler
{
    private readonly IContactRepository _repo;

    public SaveChangesHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleAsync()
    {
        await _repo.SaveChangesAsync();
    }
}
