namespace DotNet_Db_EF_Audit.Domain.Interface.Db
{
    public interface IAuditableEntity
    {
        DateTime CreatedAtUtc { get; set; }
        DateTime? UpdatedAtUtc { get; set; }
        string CreatedBy { get; set; }
        string? UpdatedBy { get; set; }
    }
}
