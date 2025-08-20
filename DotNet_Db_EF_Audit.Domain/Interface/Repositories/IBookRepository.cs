using DotNet_Db_EF_Audit.Domain.Seed;

namespace DotNet_Db_EF_Audit.Domain.Interface.Repositories
{
    public interface IBookRepository
    {
        public Task<Book> Create(Book book, CancellationToken cancellationToken);
        public Task<Book> Update(Book book, CancellationToken cancellationToken);
        public Task<Book> Get(Guid id, CancellationToken cancellationToken);
        public Task<bool?> Delete(Book book, CancellationToken cancellationToken);
    }
}
