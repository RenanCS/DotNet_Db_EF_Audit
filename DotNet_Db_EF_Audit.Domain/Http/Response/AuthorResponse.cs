namespace DotNet_Db_EF_Audit.Domain.Http.Response
{
    public sealed record AuthorResponse(Guid Id, string Name, List<BookResponse> Books);
}
