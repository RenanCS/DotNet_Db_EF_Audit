using DotNet_Db_EF_Audit.Domain.Interface.Db;

namespace DotNet_Db_EF_Audit.Domain.Seed
{
    public sealed class Author : IAuditableEntity
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public List<Book> Books { get; set; } = [];

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public string CreatedBy { get; set; } = null!;

        public string? UpdatedBy { get; set; }
    }
}
