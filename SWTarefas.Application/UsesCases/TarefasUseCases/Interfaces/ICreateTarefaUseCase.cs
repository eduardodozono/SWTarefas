using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces
{
    public interface ICreateTarefaUseCase
    {
        public Task<CreateTarefaResponse> Execute(CreateTarefaRequest tarefa, CancellationToken token = default);
    }
}
