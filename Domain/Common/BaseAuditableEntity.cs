namespace Domain.Common;

public class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public DateTimeOffset Deleted { get; set; }
}