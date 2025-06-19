using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.UnitOfWork;

namespace SWTarefas.Infrastructure.DataAcess.Dapper.RepositoryDapper.UnitOfWork
{
    public class UnitOfWorkDapper : IUnitOfWorkDapper
    {
        private readonly SWDBConnection _sWDBConnection;

        public UnitOfWorkDapper(SWDBConnection sWDBConnection)
        {
            _sWDBConnection = sWDBConnection;
        }

        public void BeginTransaction()
        {
            _sWDBConnection.Transaction = _sWDBConnection.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _sWDBConnection.Transaction.Commit();

            Dispose();
        }

        public void Rollback()
        {
            _sWDBConnection.Transaction.Rollback();

            Dispose();
        }

        public void Dispose() => _sWDBConnection.Transaction?.Dispose();
    }
}
