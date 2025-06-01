using Microsoft.AspNetCore.Mvc;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces;

namespace SWTarefas.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateTarefaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateTarefaResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromServices] ICreateTarefaUseCase _createTarefaUseCase, CreateTarefaRequest request, CancellationToken token = default)
        {
            var result = await _createTarefaUseCase.Execute(request, token);

            return Created(string.Empty, result);
        }

        [HttpDelete("{tarefaId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromServices] IDeleteTarefasUseCase _deleteTarefasUseCase, int tarefaId, CancellationToken token = default)
        {
            await _deleteTarefasUseCase.Execute(tarefaId, token);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateTarefaResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromServices] IUpdateTarefasUseCase _updateTarefasUseCase, UpdateTarefaRequest request, CancellationToken token = default)
        {
            var result = await _updateTarefasUseCase.Execute(request, token);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllTarefasUseCase _getAllTarefasUseCase, CancellationToken token = default)
        {
            var result = await _getAllTarefasUseCase.Execute(token);

            if (result == null || result.Count() == 0)
                return NoContent();

            return Ok(result);
        }


        [HttpGet("{tarefaId:int}")]
        [ProducesResponseType(typeof(GetByIdTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetByIdTarefaResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(GetByIdTarefaResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> GetById([FromServices] IGetByIdTarefasUseCase _getByIdTarefasUseCase, int tarefaId, CancellationToken token = default)
        {
            var result = await _getByIdTarefasUseCase.Execute(tarefaId, token);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
