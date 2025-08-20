using DotNet_Db_EF_Audit.Domain.Dto;
using DotNet_Db_EF_Audit.Domain.Interface.Repositories;
using DotNet_Db_EF_Audit.Domain.Interface.Service;
using DotNet_Db_EF_Audit.Domain.Mapping;

namespace DotNet_Db_EF_Audit.Application.Service
{
    public sealed class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuthorDto> Create(AuthorDto authorDto, CancellationToken cancellationToken)
        {
            var authorDb = authorDto.MapToAuthorDb();

            var newAuthorDb = await _repository.Create(authorDb, cancellationToken);

            return newAuthorDb.MapToAuthorDto();
        }

        public async Task<bool?> Delete(Guid id, CancellationToken cancellationToken)
        {
            var authorDb = await _repository.Get(id, cancellationToken);
            if (authorDb is null)
            {
                return null;
            }

            return await _repository.Delete(authorDb, cancellationToken);
        }

        public async Task<AuthorDto> Get(Guid id, CancellationToken cancellationToken)
        {
            var authorDb = await _repository.Get(id, cancellationToken);
            if (authorDb is null)
            {
                return null;
            }

            return authorDb.MapToAuthorDto();
        }

        public async Task<AuthorDto> Update(AuthorDto authorDto, CancellationToken cancellationToken)
        {
            var authorDb = await _repository.Get(authorDto.Id, cancellationToken);
            if (authorDb is null)
            {
                return null;
            }

            authorDb.Name = authorDto.Name;

            await _repository.Update(authorDb, cancellationToken);

            return authorDb.MapToAuthorDto();
        }
    }
}
