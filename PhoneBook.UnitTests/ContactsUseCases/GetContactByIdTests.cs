using Moq;
using PhoneBook.Application.Contacts.AddContact;
using PhoneBook.Application.Interfaces;

namespace PhoneBook.UnitTests.ContactsUseCases;

public class GetContactByIdTests
{
    private readonly AddContactHandler _sut;
    private readonly Mock<IContactRepository> _contactRepoMock = new Mock<IContactRepository>();

    public GetContactByIdTests()
    {
        _sut = new AddContactHandler(_contactRepoMock.Object);
    }

    [Fact]
    public async Task AddContact_ReturnsFailure_WhenResultIsNull()
    {
        // Arrange


        // Act


        // Assert

    }
}
