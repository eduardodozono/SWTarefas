using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces
{
    public interface IUpdateTarefasUseCase
    {
        public Task<UpdateTarefaResponse> Execute(UpdateTarefaRequest tarefa, CancellationToken token = default);
    }
}
