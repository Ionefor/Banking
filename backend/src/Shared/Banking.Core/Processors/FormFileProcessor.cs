using Banking.Core.Dto;
using Microsoft.AspNetCore.Http;

namespace Banking.Core.Processors;

public class FormFileProcessor : IAsyncDisposable
{
    private CreateFileDto _file;
    
    public async ValueTask DisposeAsync()
    {
        await _file.Content.DisposeAsync();
    }

    public CreateFileDto Process(IFormFile file)
    {
        var stream = file.OpenReadStream();
        _file = new CreateFileDto(stream, file.FileName);
        
        return _file;
    }
}