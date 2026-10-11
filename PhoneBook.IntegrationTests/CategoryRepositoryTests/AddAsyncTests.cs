using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Entities;
using PhoneBook.IntegrationTests.Fixtures;

namespace PhoneBook.IntegrationTests.CategoryRepositoryTests;

public class AddAsyncTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public AddAsyncTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAsync_ReturnsFailure_WhenCategoryNotUnique()
    {
        // Arrange
        var category = new ContactCategory { Name = "Family" };

        // Act
        var result = await _fixture.CategoryRepository.AddAsync(category);

        // Assert: Repository result
        Assert.True(result.IsFailure);

        // Assert: Database state
        var saved = await _fixture.Context.ContactCategories.FirstOrDefaultAsync(x => x.Name == category.Name);
        Assert.NotNull(saved);
        Assert.Equal(category.Name, saved.Name);
    }
}
