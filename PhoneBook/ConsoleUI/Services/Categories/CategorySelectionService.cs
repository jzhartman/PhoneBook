using PhoneBook.Application.Categories.DTOs;
using PhoneBook.Application.Categories.GetAllCategories;
using PhoneBook.ConsoleUI.Views;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.ConsoleUI.Services.Categories;

internal class CategorySelectionService
{
    private readonly GetAllCategoriesHandler _getAllCategoriesHandler;
    private readonly CategorySelectionView _categorySelectionView;

    public CategorySelectionService(GetAllCategoriesHandler getAllCategoriesHandler, CategorySelectionView categorySelectionView)
    {
        _getAllCategoriesHandler = getAllCategoriesHandler;
        _categorySelectionView = categorySelectionView;
    }

    internal async Task<Result<CategoryResponse>> RunAsync()
    {
        var result = await _getAllCategoriesHandler.HandleAsync();

        if (result.IsFailure)
            return Result<CategoryResponse>.Failure(result.Errors);

        if (result is null || result.Value is null)
            return Result<CategoryResponse>.Failure(new[] { Errors.GetResponseNull });

        return Result<CategoryResponse>.Success(_categorySelectionView.Render(result.Value));
    }
}
