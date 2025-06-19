using AutoMapper;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Write.Dapper
{
    public interface ICreateTarefaDapperUseCase
    {
        public Task<CreateTarefaResponse> Execute(CreateTarefaRequest tarefa, CancellationToken token = default);
    }
}
