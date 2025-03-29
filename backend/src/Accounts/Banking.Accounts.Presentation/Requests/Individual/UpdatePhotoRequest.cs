using Banking.Accounts.Application.Commands.Update.Photos;
using Banking.Core.Dto;
using Microsoft.AspNetCore.Http;

namespace Banking.Accounts.Presentation.Requests.Individual;

public record UpdatePhotoRequest(IFormFile File)
{
    public UpdatePhotoCommand ToCommand(
        Guid accountId, CreateFileDto file) => new(accountId, file);
}