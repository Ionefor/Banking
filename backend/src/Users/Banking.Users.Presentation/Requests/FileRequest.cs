using Microsoft.AspNetCore.Http;

namespace Banking.Users.Presentation.Requests;

public record FileRequest(IFormFile File);
