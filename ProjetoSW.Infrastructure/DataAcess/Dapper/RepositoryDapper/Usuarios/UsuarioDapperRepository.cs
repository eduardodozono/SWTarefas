using Dapper;
using System.Text;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Usuarios;

namespace SWTarefas.Infrastructure.DataAcess.Dapper.RepositoryDapper.Usuarios
{
    public class UsuarioDapperRepository : IUsuarioDapperReadRepository, IUsuarioDapperWriteRepository
    {
        private readonly SWDBConnection _sWDBConnection;

        public UsuarioDapperRepository(SWDBConnection sWDBConnection)
        {
            _sWDBConnection = sWDBConnection;
        }

        public async Task<bool> ExistsActiveUserWithIdentifier(Guid identifier)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("SELECT UsuarioId, UsuarioIdentifier, Nome");
            sql.AppendLine(", Senha, Email");
            sql.AppendLine("FROM Usuario");
            sql.AppendLine("WHERE UsuarioIdentifier = @identifier");

            var usuario = await connection.QueryFirstOrDefaultAsync<Usuario?>(sql.ToString(), new { identifier }, _sWDBConnection.Transaction);

            return (usuario != null);
        }

        public async Task<Usuario?> ExistsUsuarioByEmailAndPassword(string email, string password)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("SELECT UsuarioId, UsuarioIdentifier, Nome");
            sql.AppendLine(", Senha, Email");
            sql.AppendLine("FROM Usuario");
            sql.AppendLine("WHERE Email = @email");
            sql.AppendLine("AND Senha = @password");

            var usuario = await connection.QueryFirstOrDefaultAsync<Usuario?>(sql.ToString(), new { email, password }, _sWDBConnection.Transaction);

            return usuario;
        }

        public async Task<bool> ExistsUsuarioByEmail(string email)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("SELECT UsuarioId, UsuarioIdentifier, Nome");
            sql.AppendLine(", Senha, Email");
            sql.AppendLine("FROM Usuario");
            sql.AppendLine("WHERE Email = @email");

            var usuario = await connection.QueryFirstOrDefaultAsync<Usuario?>(sql.ToString(), new { email }, _sWDBConnection.Transaction);

            return (usuario != null);
        }

        public async Task<Usuario?> Create(Usuario usuario)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("INSERT INTO Usuario (UsuarioIdentifier, Nome,");
            sql.AppendLine(" Senha, Email");
            sql.AppendLine(") OUTPUT INSERTED.* VALUES (");
            sql.AppendLine("@UsuarioIdentifier");
            sql.AppendLine(", @Nome");
            sql.AppendLine(", @Senha");
            sql.AppendLine(", @Email");
            sql.AppendLine(")");

            var usuarioInserted = await connection.QueryFirstOrDefaultAsync<Usuario?>(sql.ToString(), usuario, _sWDBConnection.Transaction);

            return usuarioInserted;
        }
    }
}
