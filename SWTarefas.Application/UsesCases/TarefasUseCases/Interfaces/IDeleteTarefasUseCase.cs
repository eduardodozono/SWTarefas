namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces
{
    public interface IDeleteTarefasUseCase
    {
        public Task<int> Execute(int tarefaId, CancellationToken token = default);
    }
}
