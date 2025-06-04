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
        [ProducesResponseType(typeof(UsuariosLoginUseCaseCreateResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UsuariosLoginUseCaseCreateResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UsuariosLoginUseCaseCreateResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromServices] ILoginUsuariosUseCaseCreate loginUsuariosUseCaseCreate, [FromBody] UsuariosLoginUseCaseCreateRequest request, CancellationToken token = default)
        {
           
            return Ok();
        }
    }
}
