using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.Application.SaveChanges;

internal class SaveChangesHandler
{
    private readonly IContactRepository _repo;

    public SaveChangesHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result> HandleAsync()
    {
        var result = await _repo.SaveChangesAsync();

        if (result is null)
            return Result.Failure(Errors.SaveResponseNull);

        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return Result.Success();
    }
}
