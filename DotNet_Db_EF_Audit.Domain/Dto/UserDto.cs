namespace DotNet_Db_EF_Audit.Domain.Dto
{
    public record UserDto
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
    }
}
