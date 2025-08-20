using DotNet_Db_EF_Audit.Domain.Interface.Repositories;
using DotNet_Db_EF_Audit.Domain.Seed;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Db_EF_Audit.Infra.Db.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Author> Create(Author author, CancellationToken cancellationToken)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync(cancellationToken);

            return author;
        }

        public async Task<bool?> Delete(Author author, CancellationToken cancellationToken)
        {
            _context.Authors.Remove(author);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<Author> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Authors
             .Include(a => a.Books)
             .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        }

        public async Task<Author> Update(Author author, CancellationToken cancellationToken)
        {
            _context.Update(author);
            await _context.SaveChangesAsync(cancellationToken);

            return author;
        }
    }
}
