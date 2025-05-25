using SWTarefas.Application.UsesCases.TarefasUseCasesSite.ViewModel;

namespace SWTarefas.Application.UsesCases.TarefasUseCasesSite.Interfaces
{
    public interface IWriteTarefasUseCaseSite
    {
        public Task Create(TarefaViewModel tarefa, CancellationToken token = default);
        public Task Update(TarefaViewModel tarefa, CancellationToken token = default);
        public Task Delete(int tarefaId, CancellationToken token = default);
    }
}
