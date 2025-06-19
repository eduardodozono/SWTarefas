using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.Dapper
{
    public interface IGetByIdTarefasDapperUseCase
    {
        public Task<GetByIdTarefaResponse?> Execute(int tarefaId, CancellationToken token = default);
    }
}
