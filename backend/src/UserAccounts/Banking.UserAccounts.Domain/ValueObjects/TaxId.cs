using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.UserAccounts.Domain.ValueObjects;

public class TaxId : ComparableValueObject
{
    private TaxId() {}

    private TaxId(string taxId)
    {
        Value = taxId;
    }
    public string Value { get; }
    
    public static Result<TaxId, Error> Create(string taxId)
    {
        if (string.IsNullOrWhiteSpace(taxId) ||
            taxId.Length > Constants.Shared.MaxLowTextLength ||
            taxId.All(char.IsDigit))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(TaxId)));
        }
        
        return new TaxId(taxId);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}