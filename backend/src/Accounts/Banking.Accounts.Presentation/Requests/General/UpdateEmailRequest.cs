using Banking.Accounts.Application.Commands.Update.Mail;

namespace Banking.Accounts.Presentation.Requests.General;

public record UpdateEmailRequest(string Email)
{
    public UpdateEmailCommand ToCommand(Guid accountId)
        => new(accountId, Email);
}