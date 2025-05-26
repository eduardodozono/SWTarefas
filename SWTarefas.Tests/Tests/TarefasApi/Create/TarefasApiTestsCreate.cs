using Newtonsoft.Json;
using static SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums.StatusEnum;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using System.Net;
using System.Text;
using FluentAssertions;
using Bogus;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Tests.Tests.TarefasApi.Create
{
    public class TarefasApiTestsCreate
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private const string url = "/tarefas";
        private TarefasApiAppication application = new TarefasApiAppication();
        private HttpClient client;

        public TarefasApiTestsCreate()
        {
            client = application.CreateClient();
        }

        [Fact]
        public async Task TarefasAPI_Create_Created()
        {
            string requestTitulo = Guid.NewGuid().ToString();
            string requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, false);

            var tarefaRequest = new CreateTarefaRequest { Titulo = requestTitulo, Descricao = requestDescricao, DataConclusaoPrevista = new DateOnly(2025, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaRequest);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();
            var tarefaResponse = JsonConvert.DeserializeObject<CreateTarefaResponse>(contents);

            result.StatusCode.Should().Be(HttpStatusCode.Created);
            tarefaResponse.Should().NotBeNull();
            tarefaResponse.Status.Should().Be((int)TarefaStatus.Pendente);
            tarefaResponse.Titulo.Should().Be(requestTitulo);
            tarefaResponse.Descricao.Should().Be(requestDescricao);
        }

        [Fact]
        public async Task TarefasAPI_Create_Titulo_Vazio_Invalido_BadRequest()
        {
            string requestTitulo = string.Empty;
            string requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, false);

            var tarefaRequest = new CreateTarefaRequest { Titulo = requestTitulo, Descricao = requestDescricao, DataConclusaoPrevista = new DateOnly(2025, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaRequest);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain(SWTarefasMessagesExceptions.TituloVazio);
        }

        [Fact]
        public async Task TarefasAPI_Create_Titulo_Maximo_Caracteres_Invalido_BadRequest()
        {
            var requestTitulo = _faker.Lorem.Paragraphs(5);
            var requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, false);

            var tarefaRequest = new CreateTarefaRequest { Titulo = requestTitulo, Descricao = requestDescricao, DataConclusaoPrevista = new DateOnly(2025, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaRequest);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain(SWTarefasMessagesExceptions.TItuloMaximoCaracteres);
        }

        [Fact]
        public async Task TarefasAPI_Create_Descricao_Maximo_Caracteres_Invalido_BadRequest()
        {
            var requestTitulo = Guid.NewGuid().ToString();
            var requestDescricao = _faker.Lorem.Paragraphs(10);

            await TarefasMockData.CreateTarefas(application, false);

            var tarefaRequest = new CreateTarefaRequest { Titulo = requestTitulo, Descricao = requestDescricao, DataConclusaoPrevista = new DateOnly(2025, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaRequest);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain(SWTarefasMessagesExceptions.DescricaoMaximoCaracteres);
        }
    }
}
