using MediatR;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.MediatR.DTO.Request;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Delete
{
    public class DeleteTarefaHandler : IRequestHandler<DeleteTarefaCommandRequest, int>
    {
        private readonly ITarefaDeleteRepository _tarefaDeleteRepository;
        private readonly ITarefaReadRepository _tarefaReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DeleteTarefaHandler(ITarefaDeleteRepository tarefaDeleteRepository, ITarefaReadRepository tarefaReadRepository, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _tarefaDeleteRepository = tarefaDeleteRepository;
            _tarefaReadRepository = tarefaReadRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<int> Handle(DeleteTarefaCommandRequest request, CancellationToken token = default)
        {
            var tarefaDB = await _tarefaReadRepository.GetById(request.TarefaId);

            if (tarefaDB == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.TarefaNaoExiste);

            await _tarefaDeleteRepository.Delete(request.TarefaId, token);

            await _unitOfWork.Commit(token);

            await _mediator.Publish(new DeleteTarefaNotification(tarefaDB));

            return await Task.FromResult(request.TarefaId);
        }
    }
}
