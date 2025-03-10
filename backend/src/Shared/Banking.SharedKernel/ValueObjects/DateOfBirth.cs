using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.SharedKernel.ValueObjects;

public class DateOfBirth : ComparableValueObject
{
    private DateOfBirth() {}
    
    private DateOfBirth(DateOnly date)
    {
        Value = date;
    }

    public DateOnly Value { get; }
    
    public static Result<DateOfBirth, Error> Create(DateOnly date)
    {
        if (date.Year < Constants.Shared.MinYearBirthday ||
            date > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.ValueIsInvalid(nameof(DateOfBirth)));
        }
        
        return new DateOfBirth(date);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}