using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.UserAccounts.Domain.ValueObjects;

public class Photo : ComparableValueObject
{
    private Photo() {}
    
    private Photo(FilePath path)
    {
        Path = path;
    }
    public FilePath Path { get; } = null!;

    public static Result<Photo, Error> Create(string path)
    {
        var filePath = FilePath.Create(path);
        
        if (filePath.IsFailure)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(FilePath)));
        }

        return new Photo(filePath.Value);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Path;
    }
}