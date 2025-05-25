using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces
{
    public interface IGetByIdTarefasUseCase
    {
        public Task<GetByIdTarefaResponse?> Execute(int tarefaId, CancellationToken token = default);
    }
}
