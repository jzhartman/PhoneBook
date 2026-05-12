namespace PhoneBook.Domain.Validation.Errors;

public static class Errors
{
    public static readonly Error None = Error.None;

    public static readonly Error GenericNull = new("GenericNull", "");
    public static readonly Error AddResponseNull = new("AddResponseNull", "Add action returned null value");
    public static readonly Error DeleteResponseNull = new("DeleteResponseNull", "Delete action returned null value");
    public static readonly Error SaveResponseNull = new("SaveResponseNull", "Save action returned null value");
    public static readonly Error UpdateResponseNull = new("UpdateResponseNull", "Update action returned null value");
    public static readonly Error GetResponseNull = new("GetResponseNull", "Get action returned null value");



    public static readonly Error ContactExists = new("ContactExists", "A contact with that information already exists.");
    public static readonly Error ContactDoesNotExist = new("ContactDoesNotExist", "A contact with that information does not exist.");
    public static readonly Error ContactNotFound = new("ContactNotFound", "The selected contact was not found.");

    public static readonly Error CategoryExists = new("CategoryExists", "A category with that name already exists.");
    public static readonly Error CategoryDoesNotExist = new("CategoryDoesNotExist", "A contact with that name does not exist.");
    public static readonly Error CategoryNotFound = new("CategoryNotFound", "The selected category was not found.");

    public static readonly Error EntryNull = new("EntryNull", "This data cannot be left blank.");
    public static readonly Error InvalidEmail = new("InvalidEmail", "The email is invalid.");
    public static readonly Error InvalidPhoneNumber = new("InvalidPhoneNumber", "The phone number is invalid.");

    public static readonly Error LoadEditDataFailed = new("LoadEditDataFailed", "There was an error loading the edited data for the contact.");
    public static readonly Error UpdateDataFailed = new("UpdateDataFailed", "Database returned no records were successfully updated");
    public static readonly Error SaveDataFailed = new("SaveDataFailed", "Database returned no records were successfully saved");


    public static readonly Error DBConnectionFailure = new("DBConnectionFailure", "Failed to connect to database.");
}
