using PhoneBook.Application.Categories.DeleteCategory;

namespace PhoneBook.ConsoleUI.Services.Categories;

internal class DeleteCategoryService
{
    private readonly DeleteCategoryByIdHandler _deleteCategoryByIdHandler;

    public DeleteCategoryService(DeleteCategoryByIdHandler deleteCategoryByIdHandler)
    {
        _deleteCategoryByIdHandler = deleteCategoryByIdHandler;
    }

    internal async Task RunAsync()
    {
        // Print category list and let user select one
        // Pass category to method below
        var result = await _deleteCategoryByIdHandler.HandleAsync();

        // Handle errors
        // Confirmation??

        Console.WriteLine("Running...");
        Console.Write("Press any key");
        Console.ReadLine();
    }

    // ToDo: Left off here.
    /*  Need to flesh this out better
     *  Review error handling and validation in use case
     */
}
