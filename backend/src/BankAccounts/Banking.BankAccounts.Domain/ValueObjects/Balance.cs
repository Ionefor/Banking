using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.BankAccounts.Domain.ValueObjects;

public class Balance : ComparableValueObject
{
    private Balance() {}

    private Balance(double value)
    {
        Value = value;
    }
    
    public double Value { get; }
    
    public static Result<Balance, Error> Create(double value)
    {
        if (value < 0)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.ValueIsInvalid(nameof(Balance)));
        }
        
        return new Balance(value);
    }
    
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}