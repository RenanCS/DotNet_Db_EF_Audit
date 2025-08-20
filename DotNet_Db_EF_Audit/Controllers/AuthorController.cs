using DotNet_Db_EF_Audit.Domain.Http.Request;
using DotNet_Db_EF_Audit.Domain.Interface.Service;
using DotNet_Db_EF_Audit.Domain.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_Db_EF_Audit.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService service;

        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var authorDto = await service.Get(id, cancellationToken);
            if (authorDto is null)
            {
                return NotFound();
            }

            return Ok(authorDto.MapToAuthorResponse());
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] CreateAuthorRequest request, CancellationToken cancellationToken)
        {
            var authorDto = await service.Create(request.MapToAuthoDto(), cancellationToken);
            if (authorDto is null)
            {
                return NotFound();
            }

            return Ok(authorDto.MapToAuthorResponse());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [FromRoute] Guid id,
            [FromBody] UpdateAuthorRequest request,
            CancellationToken cancellationToken)
        {
            var authorDto = await service.Update(request.MapToAuthoDto(id), cancellationToken);
            if (authorDto is null)
            {
                return NotFound();
            }

            return Ok(authorDto.MapToAuthorResponse());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
           [FromRoute] Guid id,
           CancellationToken cancellationToken)
        {
            var excludeAuthor = await service.Delete(id, cancellationToken);
            if (excludeAuthor is null)
            {
                return NotFound();
            }

            return Ok(excludeAuthor);
        }
    }
}
