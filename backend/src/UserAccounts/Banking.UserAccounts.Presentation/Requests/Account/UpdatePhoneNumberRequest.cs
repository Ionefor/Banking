using Banking.UserAccounts.Application.Commands.Account.Update.Number;

namespace Banking.UserAccounts.Presentation.Requests.Account;

public record UpdatePhoneNumberRequest(string PhoneNumber)
{
    public UpdatePhoneNumberCommand ToCommand(Guid userId)
        => new(userId, PhoneNumber);
}