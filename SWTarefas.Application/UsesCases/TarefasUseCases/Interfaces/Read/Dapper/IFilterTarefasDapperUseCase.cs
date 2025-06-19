using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.Dapper
{
    public interface IFilterTarefasDapperUseCase
    {
        public Task<IEnumerable<GetAllTarefaResponse>?> Execute(FilterTarefaRequest request, CancellationToken token = default);
    }
}
