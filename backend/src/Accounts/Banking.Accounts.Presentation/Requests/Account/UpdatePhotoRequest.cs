using Banking.Accounts.Application.Commands.Update.Photos;
using Banking.Accounts.Contracts.Dto.Commands;
using Banking.Core.Dto;
using Microsoft.AspNetCore.Http;

namespace Banking.Accounts.Presentation.Requests.Account;

public record UpdatePhotoRequest(IFormFile File)
{
    public UpdatePhotoCommand ToCommand(
        Guid userId, CreateFileDto file) => new(userId, file);
}