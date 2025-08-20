using DotNet_Db_EF_Audit.Domain.Dto;
using DotNet_Db_EF_Audit.Domain.Interface.Repositories;
using DotNet_Db_EF_Audit.Domain.Interface.Service;
using DotNet_Db_EF_Audit.Domain.Mapping;

namespace DotNet_Db_EF_Audit.Application.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }
        public async Task<BookDto> Create(BookDto bookDto, CancellationToken cancellationToken)
        {
            var bookDb = bookDto.MapToBookDb();

            var newBookDb = await _repository.Create(bookDb, cancellationToken);

            return newBookDb.MapToBookDto();
        }

        public async Task<bool?> Delete(Guid id, CancellationToken cancellationToken)
        {
            var bookDb = await _repository.Get(id, cancellationToken);
            if (bookDb is null)
            {
                return null;
            }

            return await _repository.Delete(bookDb, cancellationToken);
        }

        public async Task<BookDto> Get(Guid id, CancellationToken cancellationToken)
        {
            var bookDb = await _repository.Get(id, cancellationToken);
            if (bookDb is null)
            {
                return null;
            }

            return bookDb.MapToBookDto();
        }

        public async Task<BookDto> Update(BookDto bookDto, CancellationToken cancellationToken)
        {
            var bookDb = await _repository.Get(bookDto.Id, cancellationToken);
            if (bookDb is null)
            {
                return null;
            }

            bookDb.Title = bookDto.Title;
            bookDb.Year = bookDto.Year;
            bookDb.AuthorId = bookDto.Author.Id;
            bookDb.Author = bookDto.Author.MapToAuthorDb();

            await _repository.Update(bookDb, cancellationToken);

            return bookDb.MapToBookDto();
        }
    }
}
