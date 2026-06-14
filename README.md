# PhoneBookApp

A simple, console-based Phone Book application built with .NET 10 using a layered clean architecture (Domain → Application → Infrastructure → Presentation). Uses Entity Framework Core with SQLite and includes an SMTP-backed email service.

## Description
This is a basic Phone Book application. It allows the user to add contacts to a database. Contacts consist of First Name, Last Name, Phone Number, Email Address and Category. Categories are user-defined and help to increase organization and simplify searches. Users are able to create, edit, or delete these contacts and categories. The user can configure SMTP email settings to allow them to directly send an email to a contact from the list.
This was built following The C# Academy [Phone Book project guidelines](https://www.thecsharpacademy.com/project/16/phonebook).

## Features
- Clean Architecture (layered as Domain → Application → Infrastructure → Presentation)
- Entity Framework Core
- SQLite database for data persistence
- Console-based UI using Spectre.Console
- Email sending to a selected contact via configurable SMTP

## Configuration
- App settings live in `appsettings.json`.
  - Connection string name: `PhoneBook` — recommended SQLite example:
    - `Data Source=phonebook.db`
  - SMTP settings section: `SmtpSettings` matching the `SmtpSettings` class:
    - `Host`, `Port`, `UseSsl`, `Username`, `Password`, `FromName`, `FromEmail`

## Usage
### Main Menu
- Opens with a Main Menu providing the options:
    <img width="1626" height="205" alt="image" src="https://github.com/user-attachments/assets/c6e96111-e2a4-44ba-8fb0-bcba04cf380b" />


### Add Contact
- Selecting Add Contact will prompt the user to input the relevant data for the contact. All fields are required:
    <img width="1623" height="319" alt="image" src="https://github.com/user-attachments/assets/39c5ee9c-9ab5-413e-9edf-9e2715517900" />

- A confirmation message is provided, after which the contact will be added or discarded.


### View Contact
- Selecting View Contact will first print the list of Categories. The user selects from the list to see only contacts in that category:
    <img width="1612" height="237" alt="image" src="https://github.com/user-attachments/assets/25490e22-9cdd-42cf-81aa-bfacdfa9c650" />

- Once the Category is selected, the relevant contact list is printed. The user selects the contact to view their details:
    <img width="1626" height="469" alt="image" src="https://github.com/user-attachments/assets/234d6c43-c2b9-4328-ba24-8bb687f5ef79" />

- Contact details are displayed below, as well as the relevant options:
    <img width="1612" height="652" alt="image" src="https://github.com/user-attachments/assets/f2c1313e-4c79-4b4d-b2ad-13cbbe3c53f0" />

- Options:
  - Delete Contact: Deleted the currently displayed contact and returns to the Main Menu (requires confirmation)
  - Edit Contact: Allows any of the paramters for the contact to be changed
  - Send Email: Sends an email to the contact at their displayed email address (requires proper SMTP configuration in appsettings.json)
  - Return to Main Menu: Returns to the Main Menu

### Manage Categories
- Selecting Manage Categories displays the following submenu:
    <img width="1618" height="205" alt="image" src="https://github.com/user-attachments/assets/dabb83cc-600c-42b5-bc8e-35b117c34afc" />

- Options:
  - Add Category: Adds a new category to the list (must have a unique name -- case insensitive)
  - Delete Category: Select a category from the list and delete it -- This will set all contacts within that category to Uncategorized (requires confirmation)
  - Rename Category: Rename the category to whatevery you want (case sensitive)
  - Return to Main Menu: Returns to the Main Menu
