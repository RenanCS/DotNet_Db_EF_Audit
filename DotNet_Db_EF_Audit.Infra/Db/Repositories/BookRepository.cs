using DotNet_Db_EF_Audit.Domain.Interface.Repositories;
using DotNet_Db_EF_Audit.Domain.Seed;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Db_EF_Audit.Infra.Db.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Book> Create(Book book, CancellationToken cancellationToken)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);

            return book;
        }

        public async Task<bool?> Delete(Book book, CancellationToken cancellationToken)
        {
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<Book> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Books
             .Include(a => a.Author)
             .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<Book> Update(Book book, CancellationToken cancellationToken)
        {
            _context.Update(book);
            await _context.SaveChangesAsync(cancellationToken);

            return book;
        }
    }
}
