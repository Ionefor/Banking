using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.ClientAccounts.Domain.ValueObjects;

public class PaymentDetails : ComparableValueObject
{
    private PaymentDetails() {}

    private PaymentDetails(string details)
    {
        Value = details;
    }
    
    public string Value { get; }
    
    public static Result<PaymentDetails, Error> Create(string details)
    {
        if (string.IsNullOrWhiteSpace(details) ||
            details.Length != Constants.Shared.MaxLowTextLength ||
            !details.All(char.IsDigit))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.ValueIsInvalid(nameof(PaymentDetails)));
        }
        
        return new PaymentDetails(details);
    }
    
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}