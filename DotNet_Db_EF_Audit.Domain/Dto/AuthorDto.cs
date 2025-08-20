namespace DotNet_Db_EF_Audit.Domain.Dto
{
    public record AuthorDto
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public List<BookDto> Books { get; init; } = new();
    }
}
