namespace PhoneBook.Domain.Validation.Errors;

public static class Errors
{
    public static readonly Error None = Error.None;

    public static readonly Error GenericNull = new("GenericNull", "");

    public static readonly Error ContactExists = new("ContactExists", "A contact with that information already exists.");
    public static readonly Error EntryNull = new("EntryNull", "This data cannot be left blank.");
    public static readonly Error InvalidEmail = new("InvalidEmail", "The email is invalid.");
    public static readonly Error InvalidPhoneNumber = new("InvalidPhoneNumber", "The phone number is invalid.");




    public static readonly Error DBConnectionFailure = new("DBConnectionFailure", "Failed to connect to database.");
}
