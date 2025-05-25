namespace SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas
{
    public interface ITarefaDeleteRepository
    {
        public Task Delete(int id, CancellationToken token = default);
    }
}
