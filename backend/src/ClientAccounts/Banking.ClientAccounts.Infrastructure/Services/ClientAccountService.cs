using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Domain.Aggregate;
using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.ClientAccounts.Infrastructure.Repositories;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.ClientAccounts.Infrastructure.Services;

public class ClientAccountService : IClientAccountService
{
    private readonly ClientReadRepository _readRepository;
    private readonly ClientWriteRepository _writeRepository;

    public ClientAccountService(
        ClientReadRepository readRepository,
        ClientWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }
    
    public async Task<Result<ClientAccount, Error>> GetClientAccountIfExist(
        Guid clientAccountId,
        CancellationToken cancellationToken = default)
    {
        var clientAccountExist = await _readRepository.
            ClientAccountExist(clientAccountId, cancellationToken);

        if (!clientAccountExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts),
                    nameof(clientAccountId), clientAccountId));
        }
        
        var clientId = ClientAccountId.Create(clientAccountId);
        
        var clientAccountResult = await _writeRepository.
            GetClientAccountById(clientId, cancellationToken);
        
        if (clientAccountResult.IsFailure)
            return clientAccountResult.Error;

        return clientAccountResult.Value;
    }
    
    public async Task<Result<BankAccount, Error>> GetBankAccountIfExist(
        Guid bankAccountId,
        CancellationToken cancellationToken = default)
    {
        var bankAccountExist = await _readRepository.
            BankAccountExist(bankAccountId, cancellationToken);

        if (!bankAccountExist)
        {
            return Errors.General.
                NotFound(new ErrorParameters.NotFound(nameof(BankAccounts),
                    nameof(bankAccountId), bankAccountId));
        }
        
        var bankId = BankAccountId.Create(bankAccountId);
        
        var bankAccountResult = await _writeRepository.
            GetBankAccountById(bankId, cancellationToken);
        
        if (bankAccountResult.IsFailure)
            return bankAccountResult.Error;

        return bankAccountResult.Value;
    }
    
    public async Task<Result<Card, Error>> GetCardIfExist(
        Guid cardId,
        CancellationToken cancellationToken = default)
    {
        var cardExist = await _readRepository.CardExist(cardId, cancellationToken);

        if (!cardExist)
        {
            return Errors.General.
                NotFound(new ErrorParameters.NotFound(nameof(Card),
                    nameof(cardId), cardId));
        }
        
        var id = CardId.Create(cardId);
        
        var cardResult = await _writeRepository.GetCardById(id, cancellationToken);
        
        if (cardResult.IsFailure)
            return cardResult.Error;

        return cardResult.Value;
    }
}