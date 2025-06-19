namespace SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task Commit(CancellationToken token = default);
    }
}
