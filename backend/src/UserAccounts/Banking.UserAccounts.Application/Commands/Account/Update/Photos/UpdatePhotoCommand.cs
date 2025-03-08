using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Commands.Account.Update.Photos;

public record UpdatePhotoCommand(Guid UserId, string FilePath) : ICommand;