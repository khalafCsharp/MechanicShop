namespace Domain.Common.Entities
{
  public abstract class AuditableEntity : BaseEntity
  {
    public string CreatedBy { get; set; } = string.Empty;
    public string LastUpdatedBy { get; set; } = string.Empty;

    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset LastUpdatedAtUtc { get; set; }

    protected AuditableEntity(Guid id) : base(id)
    {

    }


  }
}
