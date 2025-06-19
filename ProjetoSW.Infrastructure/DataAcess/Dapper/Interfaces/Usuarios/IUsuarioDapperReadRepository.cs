using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Usuarios
{
    public interface IUsuarioDapperReadRepository
    {
        public Task<bool> ExistsActiveUserWithIdentifier(Guid identifier);
        public Task<Usuario?> ExistsUsuarioByEmailAndPassword(string email, string password);
        public Task<bool> ExistsUsuarioByEmail(string email);
    }
}
