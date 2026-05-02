namespace PhoneBook.Domain.Validation.Errors;

public static class Errors
{
    public static readonly Error None = Error.None;

    public static readonly Error GenericNull = new("GenericNull", "");
    public static readonly Error SaveResponseNull = new("SaveResponseNull", "Save action returned null value");

    public static readonly Error ContactExists = new("ContactExists", "A contact with that information already exists.");
    public static readonly Error ContactDoesNotExist = new("ContactDoesNotExist", "A contact with that information does not exist.");
    public static readonly Error ContactNotFound = new("ContactNotFound", "The selected contact was not found.");

    public static readonly Error EntryNull = new("EntryNull", "This data cannot be left blank.");
    public static readonly Error InvalidEmail = new("InvalidEmail", "The email is invalid.");
    public static readonly Error InvalidPhoneNumber = new("InvalidPhoneNumber", "The phone number is invalid.");

    public static readonly Error LoadEditDataFailed = new("LoadEditDataFailed", "There was an error loading the edited data for the contact.");


    public static readonly Error DBConnectionFailure = new("DBConnectionFailure", "Failed to connect to database.");
}
