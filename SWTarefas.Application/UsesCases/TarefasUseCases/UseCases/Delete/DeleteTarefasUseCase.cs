using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.Interfaces.UnitOfWork;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Delete
{
    public class DeleteTarefasUseCase : IDeleteTarefasUseCase
    {
        private readonly ITarefaDeleteRepository _tarefaDeleteRepository;
        private readonly ITarefaReadRepository _tarefaReadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTarefasUseCase(ITarefaDeleteRepository tarefaDeleteRepository, ITarefaReadRepository tarefaReadRepository, IUnitOfWork unitOfWork)
        {
            _tarefaDeleteRepository = tarefaDeleteRepository;
            _tarefaReadRepository = tarefaReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Execute(int tarefaId, CancellationToken token = default)
        {
            var tarefaDB = await _tarefaReadRepository.GetById(tarefaId);

            if (tarefaDB == null)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.TarefaNaoExiste);

            await _tarefaDeleteRepository.Delete(tarefaId, token);

            await _unitOfWork.Commit(token);

            return tarefaId;
        }
    }
}
