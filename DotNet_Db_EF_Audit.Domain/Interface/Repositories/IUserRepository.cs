using DotNet_Db_EF_Audit.Domain.Seed;

namespace DotNet_Db_EF_Audit.Domain.Interface.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user, CancellationToken cancellationToken);
        Task<User> Update(User user, CancellationToken cancellationToken);
        Task<User> Get(Guid id, CancellationToken cancellationToken);
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
        Task<bool?> Delete(User user, CancellationToken cancellationToken);
    }
}
