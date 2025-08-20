using DotNet_Db_EF_Audit.Domain.Interface.Db;

namespace DotNet_Db_EF_Audit.Domain.Seed
{
    public sealed class Book : IAuditableEntity
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required int Year { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
    }
}
