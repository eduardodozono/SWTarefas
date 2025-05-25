using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces
{
    public interface IGetAllTarefasUseCase
    {
        public Task<IEnumerable<GetAllTarefaResponse>?> Execute(CancellationToken token = default);
    }
}