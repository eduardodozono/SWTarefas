using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.Dapper
{
    public interface IGetAllTarefasDapperUseCase
    {
        public Task<IEnumerable<GetAllTarefaResponse>?> Execute(CancellationToken token = default);
    }
}
