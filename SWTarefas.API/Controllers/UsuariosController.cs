using Microsoft.AspNetCore.Mvc;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces;

namespace SWTarefas.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuariosLoginUseCaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomUnauthorizedException), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromServices] ILoginUsuariosUseCase loginUsuariosUseCase, [FromBody] UsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var result = await loginUsuariosUseCase.Execute(request);

            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(CreateUsuariosLoginUseCaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomUnauthorizedException), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromServices] ICreateLoginUsuariosUseCase loginUsuariosUseCaseCreate, [FromBody] CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var result = await loginUsuariosUseCaseCreate.Execute(request, token);

            return Created(string.Empty, result);
        }
    }
}
