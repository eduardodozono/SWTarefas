using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.Dapper.RepositoryDapper.Usuarios
{
    public interface IUsuarioDapperWriteRepository
    {
        public Task<Usuario?> Create(Usuario usuario);
    }
}
