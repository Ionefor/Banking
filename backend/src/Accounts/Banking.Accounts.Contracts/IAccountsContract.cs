
using Banking.Accounts.Contracts.Dto.Commands;
using Banking.Accounts.Contracts.Dto.Models;
using Banking.Core.Dto;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.Accounts.Contracts;

public interface IAccountsContract
{
    public Task<UnitResult<ErrorList>> CreateIndividualAccount(Guid userId,
        FullNameDto fullName,
        AddressDto address,
        CreateFileDto file,
        string phoneNumber,
        DateOnly birthDate,
        string email,
        CancellationToken cancellationToken = default);
    
    public Task<UnitResult<ErrorList>> CreateCorporateAccount(
        Guid userId,
        AddressDto address,
        string companyName,
        string taxId,
        string contactEmail,
        string contactPhone,
        CancellationToken cancellationToken = default);

    public Task<Result<IndividualAccountDto, Error>> GetIndividualAccount(
        Guid accountId, CancellationToken cancellationToken);

    public Task<Result<CorporateAccountDto, Error>> GetCorporateAccount(
        Guid accountId, CancellationToken cancellationToken);

}