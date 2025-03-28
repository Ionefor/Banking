﻿using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.SharedKernel.Models.Abstractions;

public class BaseId<TId> : ComparableValueObject where TId : notnull
{
    public Guid Id { get; }
    protected BaseId(Guid id) => Id = id;
    public static TId NewGuid() => Create(Guid.NewGuid());
    public static TId Empty() => Create(Guid.Empty);
    public static TId Create(Guid id) => (TId)Activator.CreateInstance(typeof(TId),id)!;
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Id;
    }
}