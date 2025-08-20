using DotNet_Db_EF_Audit.Domain.Seed;

namespace DotNet_Db_EF_Audit.Domain.Interface.Repositories
{
    public interface IAuthorRepository
    {
        public Task<Author> Create(Author author, CancellationToken cancellationToken);
        public Task<Author> Update(Author author, CancellationToken cancellationToken);
        public Task<Author> Get(Guid id, CancellationToken cancellationToken);
        public Task<bool?> Delete(Author author, CancellationToken cancellationToken);
    }
}
