using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(typeof(UsuariosLoginUseCaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UsuariosLoginUseCaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromServices] ILoginUsuariosUseCase loginUsuariosUseCase, [FromBody] UsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var result = await loginUsuariosUseCase.Execute(request);

            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(CreateUsuariosLoginUseCaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateUsuariosLoginUseCaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateUsuariosLoginUseCaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromServices] ICreateLoginUsuariosUseCase loginUsuariosUseCaseCreate, [FromBody] CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var result = await loginUsuariosUseCaseCreate.Execute(request, token);

            return Created(string.Empty, result);
        }
    }
}
