using FluentAssertions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using System.Net.Http.Json;
using System.Net;

namespace SWTarefas.Tests.Tests.TarefasApi.Read
{
    public class TarefasApiTestsRead
    {
        private const string url = "/tarefas";

        [Fact]
        public async Task TarefasAPI_GetAll_OK()
        {
            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, true);


            var client = application.CreateClient();
            var result = await client.GetAsync(url);
            var resultJson = await client.GetFromJsonAsync<IEnumerable<GetAllTarefaResponse>?>(url);


            result.StatusCode.Should().Be(HttpStatusCode.OK);
            resultJson.Should().HaveCount(2);
        }

        [Fact]
        public async Task TarefasAPI_GetAll_Sem_Resultado_NoContent()
        {
            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, false);


            var client = application.CreateClient();
            var result = await client.GetAsync(url);


            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TarefasAPI_GetId_OK(int tarefaID)
        {
            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, true);

            string urlId = $"/tarefas/{tarefaID}";

            var client = application.CreateClient();
            var result = await client.GetAsync(urlId);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TarefasAPI_GetId_Sem_Resultado_NoContent(int tarefaID)
        {
            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, false);

            string urlId = $"/tarefas/{tarefaID}";

            var client = application.CreateClient();
            var result = await client.GetAsync(urlId);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
