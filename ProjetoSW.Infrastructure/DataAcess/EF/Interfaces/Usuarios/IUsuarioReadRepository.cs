using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Usuarios
{
    public interface IUsuarioReadRepository
    {
        public Task<Usuario?> ExistsUsuarioByEmailAndPassword(string email, string password, CancellationToken token = default);
        public Task<bool> ExistsActiveUserWithIdentifier(Guid identifier, CancellationToken token = default);
        public Task<bool> ExistsUsuarioByEmail(string email, CancellationToken token = default);
    }
}
