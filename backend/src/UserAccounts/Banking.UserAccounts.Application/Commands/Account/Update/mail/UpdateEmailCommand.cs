using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Commands.Account.Update.mail;

public record UpdateEmailCommand(Guid UserId, string Email) : ICommand;
