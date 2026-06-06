using PhoneBook.Application.Email;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Models;
using PhoneBook.ConsoleUI.Output;

namespace PhoneBook.ConsoleUI.Services.Email;

internal class SendEmailService
{
    private readonly Messages _messages;
    private readonly UserInput _userInput;
    private readonly SendEmailHandler _sendEmailHandler;

    public SendEmailService(Messages messages, UserInput userInput, SendEmailHandler sendEmailHandler)
    {
        _messages = messages;
        _userInput = userInput;
        _sendEmailHandler = sendEmailHandler;
    }
    internal async Task RunAsync(FullContactViewModel contact)
    {
        var subject = _userInput.GetEmailSubjectFromUser();
        var body = _userInput.GetEmailBodyFromUser();

        bool confirmEmail = _userInput.GetEmailContentsConfirmationFromUser(contact, subject, body);

        if (confirmEmail)
        {
            bool retrySend = true;
            int retryCount = 0;

            while (retrySend)
            {
                if (retryCount > 0)
                    _messages.RetryingSendEmailMessage(retryCount);

                var emailResult = await _sendEmailHandler.HandleAsync(new(contact.Email, subject, body));

                if (emailResult.IsFailure)
                {
                    _messages.ErrorMessage(emailResult.Errors);

                    retrySend = _userInput.GetRetrySendConfirmationFromUser();
                    if (retrySend) retryCount++;
                }
                else
                {
                    _messages.EmailSendSuccessfulMessage();
                    _userInput.PressAnyKeyToContinue();
                    retrySend = false;
                }
            }
        }
    }
}
