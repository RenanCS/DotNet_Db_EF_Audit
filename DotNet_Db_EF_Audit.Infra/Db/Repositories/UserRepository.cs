using DotNet_Db_EF_Audit.Domain.Interface.Repositories;
using DotNet_Db_EF_Audit.Domain.Seed;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Db_EF_Audit.Infra.Db.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<bool?> Delete(User user, CancellationToken cancellationToken)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<User> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
            .FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
        }

        public async Task<User> Update(User user, CancellationToken cancellationToken)
        {
            _context.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}
