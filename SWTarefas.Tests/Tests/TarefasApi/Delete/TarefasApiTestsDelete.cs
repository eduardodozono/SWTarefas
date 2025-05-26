using FluentAssertions;
using System.Net;

namespace SWTarefas.Tests.Tests.TarefasApi.Delete
{
    public class TarefasApiTestsDelete
    {
        private TarefasApiAppication application = new TarefasApiAppication();
        private HttpClient client;

        public TarefasApiTestsDelete()
        {
            client = application.CreateClient();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TarefasAPI_Delete_NoContent(int tarefaId)
        {
            await TarefasMockData.CreateTarefas(application, true);

            var urlDelete = $"/tarefas/{tarefaId}";

            var result = await client.DeleteAsync(urlDelete);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TarefasAPI_Delete_Registro_Nao_Encontrado_BadRequest(int tarefaId)
        {
            await TarefasMockData.CreateTarefas(application, false);

            var urlDelete = $"/tarefas/{tarefaId}";

            var result = await client.DeleteAsync(urlDelete);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
