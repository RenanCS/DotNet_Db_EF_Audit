using DotNet_Db_EF_Audit.Domain.Dto;

namespace DotNet_Db_EF_Audit.Domain.Interface.Service
{
    public interface IBookService
    {
        public Task<BookDto> Create(BookDto bookDto, CancellationToken cancellationToken);
        public Task<BookDto> Update(BookDto bookDto, CancellationToken cancellationToken);
        public Task<BookDto> Get(Guid id, CancellationToken cancellationToken);
        public Task<bool?> Delete(Guid id, CancellationToken cancellationToken);
    }
}
