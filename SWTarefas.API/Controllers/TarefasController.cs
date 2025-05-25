using Microsoft.AspNetCore.Mvc;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces;

namespace SWTarefas.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ICreateTarefaUseCase _createTarefaUseCase;
        private readonly IDeleteTarefasUseCase _deleteTarefasUseCase;
        private readonly IUpdateTarefasUseCase _updateTarefasUseCase;
        private readonly IGetAllTarefasUseCase _getAllTarefasUseCase;
        private readonly IGetByIdTarefasUseCase _getByIdTarefasUseCase;

        public TarefasController(ICreateTarefaUseCase createTarefaUseCase, IDeleteTarefasUseCase deleteTarefasUseCase, IUpdateTarefasUseCase updateTarefasUseCase, IGetAllTarefasUseCase getAllTarefasUseCase, IGetByIdTarefasUseCase getByIdTarefasUseCase)
        {
            _createTarefaUseCase = createTarefaUseCase;
            _deleteTarefasUseCase = deleteTarefasUseCase;
            _updateTarefasUseCase = updateTarefasUseCase;
            _getAllTarefasUseCase = getAllTarefasUseCase;
            _getByIdTarefasUseCase = getByIdTarefasUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTarefaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateTarefaResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateTarefaRequest request, CancellationToken token = default)
        {
            var result = await _createTarefaUseCase.Execute(request, token);

            return Created(string.Empty, result);
        }

        [HttpDelete("{tarefaId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int tarefaId, CancellationToken token = default)
        {
            await _deleteTarefasUseCase.Execute(tarefaId, token);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateTarefaResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateTarefaRequest request, CancellationToken token = default)
        {
            var result = await _updateTarefasUseCase.Execute(request, token);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var result = await _getAllTarefasUseCase.Execute(token);

            if (result == null || result.Count() == 0)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("{tarefaId:int}")]
        public async Task<IActionResult> GetById(int tarefaId, CancellationToken token = default)
        {
            var result = await _getByIdTarefasUseCase.Execute(tarefaId, token);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
