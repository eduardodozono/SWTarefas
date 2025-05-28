using Newtonsoft.Json;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using System.Net;
using System.Text;
using FluentAssertions;
using Bogus;
using SWTarefas.Resources.Resources;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SWTarefas.Tests.Tests.TarefasApi.Update
{
    public class TarefasApiTestsUpdate
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private const string url = "/tarefas";
        private TarefasApiAppication application = new TarefasApiAppication();
        private HttpClient client;

        public TarefasApiTestsUpdate()
        {
            client = application.CreateClient();
        }

        [Theory]
        [InlineData(1, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_OK(int requestTarefaId, int requestStatus)
        {
            string requestTitulo = Guid.NewGuid().ToString();
            string requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, true);

            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();
            var tarefaResponse = JsonConvert.DeserializeObject<UpdateTarefaResponse>(contents);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            tarefaResponse.Should().NotBeNull();
            tarefaResponse.Status.Should().Be(requestStatus);
            tarefaResponse.Titulo.Should().Be(requestTitulo);
            tarefaResponse.Descricao.Should().Be(requestDescricao);
        }

        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente)]
        public async Task TarefasAPI_Update_Status_Erro_Pendente_DataRealiza_Preenchida_BadRequest(int requestTarefaId, int requestStatus)
        {
            string requestTitulo = Guid.NewGuid().ToString();
            string requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, true);

            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().Contain(SWTarefasMessagesExceptions.StatusConcluidoErroApiDataRealizada);
        }

        [Theory]
        [InlineData(1, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_Status_Erro_Concluido_DataRealiza_Nao_Preenchida_BadRequest(int requestTarefaId, int requestStatus)
        {
            string requestTitulo = Guid.NewGuid().ToString();
            string requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, true);

            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().Contain(SWTarefasMessagesExceptions.StatusPendenteErroApiDataRealizadaVazia);
        }

        [Theory]
        [InlineData(998, (int)TarefaStatus.Pendente)]
        [InlineData(999, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_Tarefa_Nao_Encontrada_BadRequest(int requestTarefaId, int requestStatus)
        {
            string requestTitulo = Guid.NewGuid().ToString();
            string requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, false);

            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, contentString);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1, 998)]
        [InlineData(2, 999)]
        public async Task TarefasAPI_Update_Status_Invalido_BadRequest(int requestTarefaId, int requestStatus)
        {
            string requestTitulo = Guid.NewGuid().ToString();
            string requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, true);

            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain(SWTarefasMessagesExceptions.StatusInvalido);
        }

        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente, "")]
        [InlineData(2, (int)TarefaStatus.Concluída, "")]
        public async Task TarefasAPI_Update_Titulo_Vazio_Invalido_BadRequest(int requestTarefaId, int requestStatus, string requestTitulo)
        {
            string requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, true);

            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain(SWTarefasMessagesExceptions.TituloVazio);
        }


        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente)]
        [InlineData(2, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_Titulo_Maximo_Caracteres_Invalido_BadRequest(int requestTarefaId, int requestStatus)
        {
            var requestTitulo = _faker.Lorem.Paragraphs(5);
            var requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, true);

            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain(SWTarefasMessagesExceptions.TItuloMaximoCaracteres);
        }


        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente)]
        [InlineData(2, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_Descricao_Maximo_Caracteres_Invalido_BadRequest(int requestTarefaId, int requestStatus)
        {
            var requestTitulo = Guid.NewGuid().ToString();
            var requestDescricao = _faker.Lorem.Paragraphs(8);

            await TarefasMockData.CreateTarefas(application, true);

            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain(SWTarefasMessagesExceptions.DescricaoMaximoCaracteres);
        }

        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente)]
        [InlineData(2, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_DataPrevista_Maior_DataRealizada_Invalida_BadRequest(int requestTarefaId, int requestStatus)
        {
            var requestTitulo = Guid.NewGuid().ToString();
            var requestDescricao = Guid.NewGuid().ToString();

            await TarefasMockData.CreateTarefas(application, true);

            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2025, 1, 2), DataConclusaoRealizada = new DateOnly(2025, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain(SWTarefasMessagesExceptions.DataConclusaoInferiorDataPrevista);
        }
    }
}
