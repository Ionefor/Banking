using Banking.Core.Abstractions;
using Banking.Core.Dto;

namespace Banking.Accounts.Application.Commands.Update.Photos;

public record UpdatePhotoCommand(Guid UserId, CreateFileDto File) : ICommand;