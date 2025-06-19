using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read.EF
{
    public interface IGetAllTarefasUseCase
    {
        public Task<IEnumerable<GetAllTarefaResponse>?> Execute(CancellationToken token = default);
    }
}