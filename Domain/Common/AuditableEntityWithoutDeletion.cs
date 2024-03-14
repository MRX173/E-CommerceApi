namespace Domain.Common;

public class AuditableEntityWithoutDeletion : BaseEntity
{
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}