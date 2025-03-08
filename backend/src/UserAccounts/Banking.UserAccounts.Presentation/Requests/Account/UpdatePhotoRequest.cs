using Banking.UserAccounts.Application.Commands.Account.Update.Photos;

namespace Banking.UserAccounts.Presentation.Requests.Account;

public record UpdatePhotoRequest(string FilePath)
{
    public UpdatePhotoCommand ToCommand(Guid userId) => new(userId, FilePath);
}