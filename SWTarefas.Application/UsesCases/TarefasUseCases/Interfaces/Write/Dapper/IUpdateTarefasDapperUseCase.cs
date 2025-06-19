using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Write.Dapper
{
    public interface IUpdateTarefasDapperUseCase
    {
        public Task<UpdateTarefaResponse> Execute(UpdateTarefaRequest tarefa, CancellationToken token = default);
    }
}
