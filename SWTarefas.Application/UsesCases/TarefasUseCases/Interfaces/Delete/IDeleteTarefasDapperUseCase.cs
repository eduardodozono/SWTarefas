namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Delete
{
    public interface IDeleteTarefasDapperUseCase
    {
        public Task<int> Execute(int tarefaId, CancellationToken token = default);
    }
}
