using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.Interfaces.Usuarios
{
    public interface IUsuarioWriteRepository
    {
        public Task<Usuario?> Create(Usuario usuario, CancellationToken token = default);
    }
}
