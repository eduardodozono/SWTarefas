using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SWTarefas.Infrastructure.DataAcess.Dapper
{
    public class SWDBConnection : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public SWDBConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Banco de dados não configurado.");

            Connection = new SqlConnection(connectionString);

            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
            }
        }
    }
}
