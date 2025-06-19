using Microsoft.AspNetCore.Mvc;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Delete.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.Dapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Write.Dapper;

namespace SWTarefas.API.Controllers
{
    public class TarefasDapperController : TarefasBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateTarefaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromServices] ICreateTarefaDapperUseCase useCase, CreateTarefaRequest request, CancellationToken token = default)
        {
            var result = await useCase.Execute(request, token);

            return Created(string.Empty, result);
        }

        [HttpDelete("{tarefaId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromServices] IDeleteTarefasDapperUseCase useCase, int tarefaId, CancellationToken token = default)
        {
            await useCase.Execute(tarefaId, token);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromServices] IUpdateTarefasDapperUseCase useCase, UpdateTarefaRequest request, CancellationToken token = default)
        {
            var result = await useCase.Execute(request, token);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllTarefasDapperUseCase useCase, CancellationToken token = default)
        {
            var result = await useCase.Execute(token);

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }

        [HttpPost("filter")]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAllTarefaResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Filter([FromServices] IFilterTarefasDapperUseCase useCase, FilterTarefaRequest request, CancellationToken token = default)
        {
            var result = await useCase.Execute(request, token);

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }

        [HttpGet("{tarefaId:int}")]
        [ProducesResponseType(typeof(GetByIdTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetByIdTarefaResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdTarefasDapperUseCase useCase, int tarefaId, CancellationToken token = default)
        {
            var result = await useCase.Execute(tarefaId, token);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
