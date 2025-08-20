using DotNet_Db_EF_Audit.Domain.Dto;

namespace DotNet_Db_EF_Audit.Domain.Interface.Service
{
    public interface IAuthorService
    {
        public Task<AuthorDto> Create(AuthorDto authorDto, CancellationToken cancellationToken);
        public Task<AuthorDto> Update(AuthorDto authorDto, CancellationToken cancellationToken);
        public Task<AuthorDto> Get(Guid id, CancellationToken cancellationToken);
        public Task<bool?> Delete(Guid id, CancellationToken cancellationToken);
    }
}
