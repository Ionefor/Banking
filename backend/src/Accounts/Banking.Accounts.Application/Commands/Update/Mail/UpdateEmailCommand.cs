using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Commands.Update.Mail;

public record UpdateEmailCommand(Guid AccountId, string Email) : ICommand;
