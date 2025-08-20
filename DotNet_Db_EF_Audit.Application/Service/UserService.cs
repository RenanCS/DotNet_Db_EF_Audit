using DotNet_Db_EF_Audit.Domain.Dto;
using DotNet_Db_EF_Audit.Domain.Interface.Repositories;
using DotNet_Db_EF_Audit.Domain.Interface.Service;
using DotNet_Db_EF_Audit.Domain.Mapping;

namespace DotNet_Db_EF_Audit.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            var userDb = userDto.MapToUserDb();

            var newUserDb = await _repository.Create(userDb, cancellationToken);

            return newUserDb.MapToUserDto();
        }

        public async Task<bool?> Delete(Guid id, CancellationToken cancellationToken)
        {
            var userDb = await _repository.Get(id, cancellationToken);
            if (userDb is null)
            {
                return null;
            }

            return await _repository.Delete(userDb, cancellationToken);
        }

        public async Task<UserDto> Get(Guid id, CancellationToken cancellationToken)
        {
            var userDb = await _repository.Get(id, cancellationToken);
            if (userDb is null)
            {
                return null;
            }

            return userDb.MapToUserDto();
        }

        public async Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var userDb = await _repository.GetByEmail(email, cancellationToken);
            if (userDb is null)
            {
                return null;
            }

            return userDb.MapToUserDto();
        }

        public async Task<UserDto> Update(UserDto userDto, CancellationToken cancellationToken)
        {
            var userDb = await _repository.Get(userDto.Id, cancellationToken);
            if (userDb is null)
            {
                return null;
            }

            userDb.Email = userDto.Email;

            await _repository.Update(userDb, cancellationToken);

            return userDb.MapToUserDto();
        }
    }
}
