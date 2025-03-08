using Banking.BankAccounts.Application.Command.Accounts.Create;

namespace Banking.BankAccounts.Presentation.Requests.Accounts;

public record CreateAccountRequest(Guid UserAccountId)
{
    public CreateAccountCommand ToCommand() => new(UserAccountId);
}