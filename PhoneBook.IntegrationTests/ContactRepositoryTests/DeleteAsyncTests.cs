using PhoneBook.IntegrationTests.Fixtures;

namespace PhoneBook.IntegrationTests.ContactRepositoryTests;

public class DeleteAsyncTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public DeleteAsyncTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFailure_IfContactDoesNotExist()
    {
        // Arrange
        _fixture.ResetDatabase();

    }
}
/*
        if (!(await ContactExists(contact)))
            return Result.Failure(ContactRepositoryErrors.ContactDoesNotExist);

        await _context.Contacts
            .Where(c => c.Id == contact.Id)
            .private ExecuteDeleteAsync();

        return Result.Success();
*/