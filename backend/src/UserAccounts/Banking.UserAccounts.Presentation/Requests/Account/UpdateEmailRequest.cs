using Banking.UserAccounts.Application.Commands.Account.Update.mail;

namespace Banking.UserAccounts.Presentation.Requests.Account;

public record UpdateEmailRequest(string Email)
{
    public UpdateEmailCommand ToCommand(Guid userId)
        => new(userId, Email);
}