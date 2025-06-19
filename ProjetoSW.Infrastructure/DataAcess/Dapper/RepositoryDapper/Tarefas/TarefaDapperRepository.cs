using System.Data;
using System.Text;
using Dapper;
using SWTarefas.Domain.DTO.Tarefas;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Tarefas;

namespace SWTarefas.Infrastructure.DataAcess.Dapper.RepositoryDapper.Tarefas
{
    public class TarefaDapperRepository : ITarefaReadDapperRepository, ITarefaDeleteDapperRepository, ITarefaWriteDapperRepository
    {
        private readonly SWDBConnection _sWDBConnection;

        public TarefaDapperRepository(SWDBConnection sWDBConnection)
        {
            _sWDBConnection = sWDBConnection;
        }

        public async Task<Tarefa> Create(Tarefa tarefa, CancellationToken token = default)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("INSERT INTO Tarefa (Titulo, Descricao,");
            sql.AppendLine("DataConclusaoPrevista, DataConclusaoRealizada, Status) OUTPUT INSERTED.* VALUES (");
            sql.AppendLine("@Titulo, @Descricao, @DataConclusaoPrevista,");
            sql.AppendLine("@DataConclusaoRealizada, @Status");
            sql.AppendLine(")");

            var result = await connection.QueryFirstOrDefaultAsync<Tarefa>(sql.ToString(), tarefa, _sWDBConnection.Transaction);

            return result!;
        }

        public async Task<Tarefa> Update(Tarefa tarefa)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("UPDATE Tarefa SET Titulo = @Titulo");
            sql.AppendLine(", Descricao = @Descricao");
            sql.AppendLine(", DataConclusaoPrevista = @DataConclusaoPrevista");
            sql.AppendLine(", DataConclusaoRealizada = @DataConclusaoRealizada");
            sql.AppendLine(", Status = @Status");
            sql.AppendLine("OUTPUT INSERTED.*");
            sql.AppendLine("WHERE TarefaId = @TarefaId");

            var result = await connection.QueryFirstOrDefaultAsync<Tarefa>(sql.ToString(), tarefa, _sWDBConnection.Transaction);

            return result!;
        }

        public async Task Delete(int TarefaId, CancellationToken token = default)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("DELETE FROM Tarefa");
            sql.AppendLine("WHERE TarefaId = @TarefaId");

            await connection.ExecuteAsync(sql.ToString(), new { TarefaId }, _sWDBConnection.Transaction);
        }

        public async Task<IEnumerable<Tarefa>?> Filter(FilterTarefa request)
        {
            using var connection = _sWDBConnection.Connection;
            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("SELECT TarefaId, Titulo, Descricao, Status");
            sql.AppendLine("FROM Tarefa");
            sql.AppendLine("WHERE 0 = 0");
            if (request.TarefaId > 0)
            {
                sql.AppendLine("AND TarefaId = @TarefaId");
            }
            if (!string.IsNullOrEmpty(request.Titulo))
            {
                sql.AppendLine("AND Titulo LIKE CONCAT('%',@Titulo,'%')");
            }
            if (!string.IsNullOrEmpty(request.Descricao))
            {
                sql.AppendLine("AND Titulo lIKE CONCAT('%',@Descricao,'%')");
            }
            if (request.Status > 0)
            {
                sql.AppendLine("AND Status = @Status");
            }
            sql.AppendLine("ORDER BY DataConclusaoPrevista DESC");

            var listaTarefas = await connection.QueryAsync<Tarefa>(sql.ToString(), new { request.TarefaId, request.Titulo, request.Descricao, request.Status }, _sWDBConnection.Transaction);

            return listaTarefas;
        }

        public async Task<IEnumerable<Tarefa>?> GetAll(CancellationToken token = default)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("SELECT TarefaId, Titulo, Descricao, Status");
            sql.AppendLine(", DataConclusaoPrevista, DataConclusaoRealizada");
            sql.AppendLine("FROM Tarefa");
            sql.AppendLine("ORDER BY DataConclusaoPrevista DESC");

            var listaTarefas = await connection.QueryAsync<Tarefa>(sql.ToString(), _sWDBConnection.Transaction, _sWDBConnection.Transaction);

            return listaTarefas;
        }

        public async Task<Tarefa?> GetById(int tarefaId, CancellationToken token = default)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("SELECT TarefaId, Titulo, Descricao, Status");
            sql.AppendLine(", DataConclusaoPrevista, DataConclusaoRealizada");
            sql.AppendLine("FROM Tarefa");
            sql.AppendLine("WHERE TarefaId = @tarefaId");

            var tarefa = await connection.QueryFirstOrDefaultAsync<Tarefa?>(sql.ToString(), new { tarefaId }, _sWDBConnection.Transaction);

            return tarefa;
        }

        public async Task<TarefasUsuarios?> GetTarefasUsuarios(CancellationToken token = default)
        {
            using var connection = _sWDBConnection.Connection;

            var sql = new StringBuilder();

            sql.Clear();
            sql.AppendLine("SELECT TarefaId, Titulo, Descricao, Status");
            sql.AppendLine(", DataConclusaoPrevista, DataConclusaoRealizada");
            sql.AppendLine("FROM [Tarefa]");
            sql.AppendLine("ORDER BY Status DESC");
            sql.AppendLine("");
            sql.AppendLine("SELECT UsuarioId, UsuarioIdentifier, Nome");
            sql.AppendLine(", Senha, Email");
            sql.AppendLine("FROM [Usuario]");
            sql.AppendLine("ORDER BY NOME");

            var result = await connection.QueryMultipleAsync(sql.ToString(), _sWDBConnection.Transaction);

            var tarefas = await result.ReadAsync<Tarefa>();
            var usuarios = await result.ReadAsync<Usuario>();

            return new TarefasUsuarios() { Tarefas = tarefas, Usuarios = usuarios };
        }

        protected Task<T> Execute<T>(Func<IDbConnection, Task<T>> funcao)
        {
            using var connection = _sWDBConnection.Connection;

            var result = funcao(connection);

            return result;
        }
    }
}
