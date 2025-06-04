using Microsoft.EntityFrameworkCore;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Usuarios;

namespace SWTarefas.Infrastructure.DataAcess.Repository.Usuarios
{
    public class UsuarioRepository : IUsuarioReadRepository
    {
        private readonly SWTarefasContext _context;

        public UsuarioRepository(SWTarefasContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsActiveUserWithIdentifier(Guid identifier, CancellationToken token = default)
        {
            return await _context.Usuarios.AsNoTracking().AnyAsync(usuario => usuario.UsuarioIdentifier.Equals(identifier), token);
        }

        public async Task<Usuario?> ExistsUsuarioByEmailAndPassword(string email, string password, CancellationToken token = default)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.Email.Equals(email) && usuario.Senha.Equals(password), token);
        }
    }
}
