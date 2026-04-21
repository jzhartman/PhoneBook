namespace PhoneBook.Domain.Validation.Errors;

public static class ConnectionErrors
{
    public static readonly Error None = Error.None;
    public static readonly Error DBConnectionFailure = new("DBConnectionFailure", "Failed to connect to database");
}
