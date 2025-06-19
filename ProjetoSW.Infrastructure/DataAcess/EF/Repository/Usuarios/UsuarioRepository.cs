using Microsoft.EntityFrameworkCore;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Usuarios;

namespace SWTarefas.Infrastructure.DataAcess.EF.Repository.Usuarios
{
    public class UsuarioRepository : IUsuarioReadRepository, IUsuarioWriteRepository
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

        public async Task<bool> ExistsUsuarioByEmail(string email, CancellationToken token = default)
        {
            return await _context.Usuarios.AsNoTracking().AnyAsync(usuario => usuario.Email.Equals(email), token);
        }

        public async Task<Usuario?> Create(Usuario usuario, CancellationToken token = default)
        {
            await _context.Usuarios.AddAsync(usuario, token);

            return usuario;
        }
    }
}
