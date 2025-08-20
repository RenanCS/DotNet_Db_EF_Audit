using DotNet_Db_EF_Audit.Domain.Http.Request;
using DotNet_Db_EF_Audit.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_Db_EF_Audit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationLocalService service;

        public AuthorizationController(IAuthorizationLocalService service)
        {
            this.service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {
            var token = await service.Login(request.Email, cancellationToken);
            if (token is null)
            {
                return NotFound("User not found");
            }

            return Ok(new { Token = token });
        }
    }
}
