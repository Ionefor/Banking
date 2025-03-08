using Banking.Core.Abstractions;
using Banking.SharedKernel;
using Banking.UserAccounts.Contracts.Dto;

namespace Banking.UserAccounts.Application.Command.Auth.Register;

public record RegisterUserCommand(
    AccountType AccountType,
    IndividualAccountDto? IndividualAccountDto,
    CorporateAccountDto? CorporateAccountDto) : ICommand;