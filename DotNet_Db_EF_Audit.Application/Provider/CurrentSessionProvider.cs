using DotNet_Db_EF_Audit.Domain.Interface.Provider;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DotNet_Db_EF_Audit.Application.Provider
{
    public class CurrentSessionProvider : ICurrentSessionProvider
    {
        private readonly Guid? _currentUserId;

        public CurrentSessionProvider(IHttpContextAccessor accessor)
        {
            var userId = accessor.HttpContext?.User.FindFirstValue("userid");
            if (userId is null)
            {
                return;
            }

            _currentUserId = Guid.TryParse(userId, out var guid) ? guid : null;
        }

        public Guid? GetUserId() => _currentUserId;
    }

}
