using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Read
{
    public interface IGetByIdTarefasUseCase
    {
        public Task<GetByIdTarefaResponse?> Execute(int tarefaId, CancellationToken token = default);
    }
}
