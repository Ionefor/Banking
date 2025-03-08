using Banking.Core.Abstractions;
using Banking.SharedKernel;
using Banking.UserAccounts.Contracts.Dto.Commands;

namespace Banking.UserAccounts.Application.Commands.Auth.Register;

public record RegisterUserCommand(
    AccountType AccountType,
    IndividualAccountDto? IndividualAccountDto,
    CorporateAccountDto? CorporateAccountDto) : ICommand;