namespace SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas
{
    public interface ITarefaDeleteRepository
    {
        public Task Delete(int id, CancellationToken token = default);
    }
}
