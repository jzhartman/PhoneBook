using PhoneBook.Application.Categories.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Validation;

namespace PhoneBook.Application.Categories.GetAllCategories;

internal class GetAllCategoriesHandler
{
    private readonly ICategoryRepository _repo;

    public GetAllCategoriesHandler(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<List<CategoryResponse>>> HandleAsync()
    {
        var result = await _repo.GetAllAsync();

        if (result is null || result.Value is null)
            return Result<List<CategoryResponse>>.Success(new List<CategoryResponse>());

        if (result.IsFailure)
            return Result<List<CategoryResponse>>.Failure(result.Errors);

        return Result<List<CategoryResponse>>.Success(MapToCategoryResponse(result.Value));
    }

    private List<CategoryResponse> MapToCategoryResponse(List<ContactCategory> categories)
    {
        var categoryResponseList = new List<CategoryResponse>();

        foreach (var category in categories)
        {
            categoryResponseList.Add(new CategoryResponse
            (
                category.Id,
                category.Name
            ));
        }

        return categoryResponseList;
    }
}
