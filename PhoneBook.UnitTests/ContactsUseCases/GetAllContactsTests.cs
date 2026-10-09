using Moq;
using PhoneBook.Application.Contacts.GetAllContacts;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.UnitTests.ContactsUseCases;

public class GetAllContactsTests
{
    private readonly GetAllContactsHandler _sut;
    private readonly Mock<IContactRepository> _contactRepoMock = new Mock<IContactRepository>();

    public GetAllContactsTests()
    {
        _sut = new GetAllContactsHandler(_contactRepoMock.Object);
    }

    [Fact]
    public async Task GetAllContacts_ReturnsFailure_WhenResultIsNull()
    {
        // Arrange
        _contactRepoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(() => null);

        // Act
        var result = await _sut.HandleAsync();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(ContactRepositoryErrors.ContactNotFound, result.Errors);
    }

    [Fact]
    public async Task GetAllContacts_ReturnsFailure_WhenResultIsFailure()
    {
        // Arrange
        var error = new Error("TestCode", "TestMessage");
        _contactRepoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(Result<List<Contact>>.Failure(error));

        // Act
        var result = await _sut.HandleAsync();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(error, result.Errors);
    }

    [Fact]
    public async Task GetAllContacts_ReturnsFailure_WhenResultValueIsNull()
    {
        // Arrange
        _contactRepoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(Result<List<Contact>>.Success(null));

        // Act
        var result = await _sut.HandleAsync();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(ContactRepositoryErrors.ContactNotFound, result.Errors);
    }

    [Fact]
    public async Task GetAllContacts_ReturnsFailure_WhenResultValueIsEmpty()
    {
        // Arrange
        _contactRepoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(Result<List<Contact>>.Success(new List<Contact>()));

        // Act
        var result = await _sut.HandleAsync();

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(ContactRepositoryErrors.ContactNotFound, result.Errors);
    }

    //[Fact]
    //public async Task GetAllContacts_ReturnsSuccess_WhenResultIsSuccessful()
    //{
    //    // Arrange
    //    _contactRepoMock
    //        .Setup(x => x.GetAllAsync())
    //        .ReturnsAsync(Result<List<Contact>>.Success());

    //    // Act
    //    var result = await _sut.HandleAsync();

    //    // Assert
    //    Assert.True(result.IsSuccess);
    //}
}

/*
 * 
        if (result is null)
            return Result<List<ContactResponse>>.Failure(ContactRepositoryErrors.ContactNotFound);

        if (result.IsFailure)
            return Result<List<ContactResponse>>.Failure(result.Errors);

        if (result.Value is null || result.Value.Count < 1)
            return Result<List<ContactResponse>>.Failure(ContactRepositoryErrors.ContactNotFound);

        return Result<List<ContactResponse>>.Success(ContactResponseHelper.MapToContactResponse(result.Value));
 */