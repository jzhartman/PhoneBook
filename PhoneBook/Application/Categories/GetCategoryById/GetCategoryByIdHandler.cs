using PhoneBook.Application.Categories.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.Application.Categories.GetCategoryById;

internal class GetCategoryByIdHandler
{
    private readonly ICategoryRepository _repo;

    public GetCategoryByIdHandler(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<CategoryResponse>> HandleAsync(int categoryId)
    {
        var result = await _repo.GetByIdAsync(categoryId);

        if (result is null || result.Value is null)
            return Result<CategoryResponse>.Failure(Errors.CategoryNotFound);

        if (result.IsFailure)
            return Result<CategoryResponse>.Failure(result.Errors);

        return Result<CategoryResponse>.Success(new CategoryResponse
            (
                result.Value.Id,
                result.Value.Name
            ));
    }
}
