using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.BankAccounts.Domain.ValueObjects;

public class Ccv : ComparableValueObject
{
    private Ccv() {}

    private Ccv(string code)
    {
        Value = code;
    }
    
    public string Value { get; }
    
    public static Result<Ccv, Error> Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length != 3 || code.All(char.IsDigit))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.ValueIsInvalid(nameof(Ccv)));
        }
        
        return new Ccv(code);
    }
    
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}