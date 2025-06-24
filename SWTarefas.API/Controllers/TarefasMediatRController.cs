using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWTarefas.API.Controllers.Bases;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.MediatR.DTO.Request;
using SWTarefas.Application.UsesCases.MediatR.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Write.EF;

namespace SWTarefas.API.Controllers
{
    public class TarefasMediatRController : TarefasBaseController
    {
        private readonly IMediator _mediator;

        public TarefasMediatRController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTarefaMResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateMediatR(CreateTarefaCommandRequest request, CancellationToken token = default)
        {
            var result = await _mediator.Send(request, token);

            return Created(string.Empty, result);
        }

        [HttpDelete("{tarefaId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(DeleteTarefaCommandRequest request, int tarefaId, CancellationToken token = default)
        {
            request.TarefaId = tarefaId;

            await _mediator.Send(request);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateTarefaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomBadRequestException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromServices] IUpdateTarefasUseCase useCase, UpdateTarefaRequest request, CancellationToken token = default)
        {
            var result = await useCase.Execute(request, token);

            return Ok(result);
        }
    }
}
