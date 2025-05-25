namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces
{
    public interface IDeleteTarefasUseCase
    {
        public Task Execute(int tarefaId, CancellationToken token = default);
    }
}
