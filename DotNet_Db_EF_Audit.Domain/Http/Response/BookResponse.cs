namespace DotNet_Db_EF_Audit.Domain.Http.Response
{
    public sealed record BookResponse(Guid Id, string Title, int Year, Guid? AuthorId);
}
