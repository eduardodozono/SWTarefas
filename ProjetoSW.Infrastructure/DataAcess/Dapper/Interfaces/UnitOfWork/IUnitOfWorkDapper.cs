namespace SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkDapper : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
