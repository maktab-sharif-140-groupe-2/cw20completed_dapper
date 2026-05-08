namespace CW20.Domain;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.Now;

    protected abstract void Validate();
}
