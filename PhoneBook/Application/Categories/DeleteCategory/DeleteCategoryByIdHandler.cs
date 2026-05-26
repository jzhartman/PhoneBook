using PhoneBook.Application.Categories.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.Application.Categories.DeleteCategory;

internal class DeleteCategoryByIdHandler
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IContactRepository _contactRepo;

    public DeleteCategoryByIdHandler(ICategoryRepository categoryRepo, IContactRepository contactRepo)
    {
        _categoryRepo = categoryRepo;
        _contactRepo = contactRepo;
    }

    internal async Task<Result> HandleAsync(CategoryResponse categoryResponse)
    {
        var category = new ContactCategory
        {
            Id = categoryResponse.Id,
            Name = categoryResponse.Name
        };

        var setContactsToDefaultResponse = await _contactRepo.SetCategoryToDefaultByCategoryIdAsync(category);

        if (setContactsToDefaultResponse is null)
            return Result.Failure(Errors.GenericNull);

        var deleteCategoryResponse = await _categoryRepo.DeleteAsync(category);

        if (deleteCategoryResponse is null)
            return Result.Failure(Errors.DeleteResponseNull);
        if (deleteCategoryResponse.IsFailure)
            return Result.Failure(deleteCategoryResponse.Errors);

        return Result.Success();
    }
}
