using Banking.Core.Extension;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.UserAccounts.Domain.ValueObjects;

public class PhoneNumber : ComparableValueObject
{
    private PhoneNumber() {}

    private PhoneNumber(string phoneNumber)
    {
        Value = phoneNumber;
    }
    public string Value { get; }
    
    public static Result<PhoneNumber, Error> Create(string phoneNumber)
    {
        if (!phoneNumber.IsValidPhoneNumber())
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(PhoneNumber)));
        }
        
        return new PhoneNumber(phoneNumber);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}