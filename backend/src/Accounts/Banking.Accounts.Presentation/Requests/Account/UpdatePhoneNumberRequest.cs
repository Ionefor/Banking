using Banking.Accounts.Application.Commands.Update.Number;

namespace Banking.Accounts.Presentation.Requests.Account;

public record UpdatePhoneNumberRequest(string PhoneNumber)
{
    public UpdatePhoneNumberCommand ToCommand(Guid userId)
        => new(userId, PhoneNumber);
}