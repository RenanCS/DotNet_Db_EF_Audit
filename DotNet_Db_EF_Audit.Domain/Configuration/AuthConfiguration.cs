namespace DotNet_Db_EF_Audit.Domain.Configuration
{
    public record AuthConfiguration
    {
        public required string Key { get; init; }
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
    }
}
