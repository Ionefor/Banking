using Banking.Accounts.Application.Commands.Update.Mail;

namespace Banking.Accounts.Presentation.Requests.Account;

public record UpdateEmailRequest(string Email)
{
    public UpdateEmailCommand ToCommand(Guid userId)
        => new(userId, Email);
}