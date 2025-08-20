using DotNet_Db_EF_Audit.Domain.Configuration;
using DotNet_Db_EF_Audit.Domain.Extensions;
using DotNet_Db_EF_Audit.Domain.Interface.Service;

namespace DotNet_Db_EF_Audit.Application.Service
{
    public sealed class AuthorizationLocalService : IAuthorizationLocalService
    {
        private readonly IUserService _userService;
        private readonly AuthConfiguration _authConfiguration;

        public AuthorizationLocalService(IUserService userService, AuthConfiguration authConfiguration)
        {
            _userService = userService;
            _authConfiguration = authConfiguration;
        }

        public async Task<string> Login(string email, CancellationToken cancellationToken)
        {
            var userDto = await _userService.GetByEmail(email, cancellationToken);
            if (userDto is null)
            {
                return null;
            }


            var token = JwtExtensions.GenerateJwtToken(userDto, _authConfiguration);
            return token.ToString();
        }
    }
}
