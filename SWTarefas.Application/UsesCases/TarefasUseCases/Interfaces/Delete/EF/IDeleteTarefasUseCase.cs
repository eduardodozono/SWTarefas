namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Delete.EF
{
    public interface IDeleteTarefasUseCase
    {
        public Task<int> Execute(int tarefaId, CancellationToken token = default);
    }
}
