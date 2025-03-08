using CSharpFunctionalExtensions;

namespace Banking.SharedKernel.Models.Abstractions;

public abstract class SoftDeletableEntity<TId> : Entity<TId> where TId : BaseId<TId>
{
    protected SoftDeletableEntity(TId id) : base(id){}
    public bool IsDeleted { get; private set; }
    public DateTime? DeletionDate { get; protected set; }

    protected virtual void Delete()
    {
        IsDeleted = true;
        DeletionDate = DateTime.UtcNow;
    }

    protected virtual void Restore()
    {
        IsDeleted = false;
        DeletionDate = null;
    }
}