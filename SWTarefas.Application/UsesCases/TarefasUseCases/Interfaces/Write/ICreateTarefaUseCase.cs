using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Write
{
    public interface ICreateTarefaUseCase
    {
        public Task<CreateTarefaResponse> Execute(CreateTarefaRequest tarefa, CancellationToken token = default);
    }
}
