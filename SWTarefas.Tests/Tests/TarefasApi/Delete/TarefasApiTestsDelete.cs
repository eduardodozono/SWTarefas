using FluentAssertions;
using System.Net;

namespace SWTarefas.Tests.Tests.TarefasApi.Delete
{
    public class TarefasApiTestsDelete
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TarefasAPI_Delete_NoContent(int tarefaId)
        {
            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, true);

            var urlDelete = $"/tarefas/{tarefaId}";

            var client = application.CreateClient();
            var result = await client.DeleteAsync(urlDelete);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TarefasAPI_Delete_Registro_Nao_Encontrado_BadRequest(int tarefaId)
        {
            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, false);

            var urlDelete = $"/tarefas/{tarefaId}";

            var client = application.CreateClient();
            var result = await client.DeleteAsync(urlDelete);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
