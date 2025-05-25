using Newtonsoft.Json;
using static SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums.StatusEnum;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using System.Net;
using System.Text;
using FluentAssertions;

namespace SWTarefas.Tests.Tests.TarefasApi.Create
{
    public class TarefasApiTestsCreate
    {
        private const string url = "/tarefas";

        [Theory]
        [InlineData("Titulo 1", "Decricao 1")]
        [InlineData("Titulo 2", "Decricao 2")]
        public async Task TarefasAPI_Create_Created(string requestTitulo, string requestDescricao)
        {
            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, false);


            var tarefaRequest = new CreateTarefaRequest { Titulo = requestTitulo, Descricao = requestDescricao, DataConclusaoPrevista = new DateOnly(2025, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaRequest);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
            var result = await client.PostAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();
            var tarefaResponse = JsonConvert.DeserializeObject<CreateTarefaResponse>(contents);


            result.StatusCode.Should().Be(HttpStatusCode.Created);
            tarefaResponse.Should().NotBeNull();
            tarefaResponse.Status.Should().Be((int)TarefaStatus.Pendente);
            tarefaResponse.Titulo.Should().Be(requestTitulo);
            tarefaResponse.Descricao.Should().Be(requestDescricao);
        }

        [Theory]
        [InlineData("", "Decricao Titulo Invalido 1")]
        [InlineData("", "")]
        public async Task TarefasAPI_Create_Titulo_Vazio_Invalido_BadRequest(string requestTitulo, string requestDescricao)
        {
            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, false);


            var tarefaRequest = new CreateTarefaRequest { Titulo = requestTitulo, Descricao = requestDescricao, DataConclusaoPrevista = new DateOnly(2025, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaRequest);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
            var result = await client.PostAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain("O campo titulo não pode fica vazio.");
        }

        [Theory]
        [InlineData("Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido  Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido", "Decricao Titulo Invalido 1")]
        [InlineData("Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido  Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido  Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido", "")]
        public async Task TarefasAPI_Create_Titulo_Maximo_Caracteres_Invalido_BadRequest(string requestTitulo, string requestDescricao)
        {
            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, false);


            var tarefaRequest = new CreateTarefaRequest { Titulo = requestTitulo, Descricao = requestDescricao, DataConclusaoPrevista = new DateOnly(2025, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaRequest);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
            var result = await client.PostAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain("O campo titulo tem no máximo 100 cacteres.");
        }
    }
}
