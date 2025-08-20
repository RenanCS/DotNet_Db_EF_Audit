using DotNet_Db_EF_Audit.Domain.Dto;

namespace DotNet_Db_EF_Audit.Domain.Interface.Service
{
    public interface IUserService
    {
        public Task<UserDto> Create(UserDto userDto, CancellationToken cancellationToken);
        public Task<UserDto> Update(UserDto userDto, CancellationToken cancellationToken);
        public Task<UserDto> Get(Guid id, CancellationToken cancellationToken);
        public Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken);
        public Task<bool?> Delete(Guid id, CancellationToken cancellationToken);
    }
}
