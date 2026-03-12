namespace Domain.Common.Entities
{
  public abstract class BaseEntity
  {
    public Guid Id { get; }

    public readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

    protected BaseEntity(Guid id)
    {
      if (id == Guid.Empty)
        id = Guid.NewGuid();

      this.Id = id;
    }

    public void AddDomainEvent(DomainEvent e)
    {
      _domainEvents.Add(e);

    }
    public void RemoveDomainEvent(DomainEvent e)
    {
      _domainEvents.Remove(e);
    }

    public void ClearDomainEvents()
    {
      _domainEvents.Clear();
    }
  }
}
