using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.SharedKernel.ValueObjects;

public class FullName : ComparableValueObject
{
    private FullName() {}
    private FullName(string firstName, string middleName, string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }
    public string FirstName { get; }
    public string MiddleName { get; }
    public string LastName { get; }

    public static Result<FullName, Error> Create(string firstName, string middleName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(middleName) ||
            string.IsNullOrWhiteSpace(lastName))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.ValueIsInvalid(nameof(FullName)));
        }
        
        return new FullName(firstName, middleName, lastName);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return FirstName;
        yield return MiddleName;
        yield return LastName;
    }
}