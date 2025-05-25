using Newtonsoft.Json;
using static SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums.StatusEnum;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using System.Net;
using System.Text;
using FluentAssertions;
using Bogus;

namespace SWTarefas.Tests.Tests.TarefasApi.Update
{
    public class TarefasApiTestsUpdate
    {
        private readonly Faker _faker = new Faker("pt_BR");
        private const string url = "/tarefas";

        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente)]
        [InlineData(2, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_OK(int requestTarefaId, int requestStatus)
        {
            string requestTitulo = Guid.NewGuid().ToString();
            string requestDescricao = Guid.NewGuid().ToString();

            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, true);


            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
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
        [InlineData(998, (int)TarefaStatus.Pendente)]
        [InlineData(999, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_Tarefa_Nao_Encontrada_BadRequest(int requestTarefaId, int requestStatus)
        {
            string requestTitulo = Guid.NewGuid().ToString();
            string requestDescricao = Guid.NewGuid().ToString();

            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, false);


            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
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

            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, true);


            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();


            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain("Status inválido (1 - Concluída, 2 - Pendente)");
        }

        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente, "")]
        [InlineData(2, (int)TarefaStatus.Concluída, "")]
        public async Task TarefasAPI_Update_Titulo_Vazio_Invalido_BadRequest(int requestTarefaId, int requestStatus, string requestTitulo)
        {
            string requestDescricao = Guid.NewGuid().ToString();

            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, true);


            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();


            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain("O campo título não pode fica vazio.");
        }


        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente)]
        [InlineData(2, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_Titulo_Maximo_Caracteres_Invalido_BadRequest(int requestTarefaId, int requestStatus)
        {
            var requestTitulo = _faker.Lorem.Paragraphs(5);
            var requestDescricao = Guid.NewGuid().ToString();

            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, true);


            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();


            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain("O campo título tem no máximo 100 cacteres.");
        }


        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente)]
        [InlineData(2, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_Descricao_Maximo_Caracteres_Invalido_BadRequest(int requestTarefaId, int requestStatus)
        {
            var requestTitulo = Guid.NewGuid().ToString();
            var requestDescricao = _faker.Lorem.Paragraphs(8);

            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, true);


            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2026, 1, 1), DataConclusaoRealizada = new DateOnly(2026, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();


            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain("O campo descrição tem no máximo 400 cacteres.");
        }

        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente)]
        [InlineData(2, (int)TarefaStatus.Concluída)]
        public async Task TarefasAPI_Update_DataPrevista_DataRealizada_Invalida_BadRequest(int requestTarefaId, int requestStatus)
        {
            var requestTitulo = Guid.NewGuid().ToString();
            var requestDescricao = Guid.NewGuid().ToString();

            await using var application = new TarefasApiAppication();
            await TarefasMockData.CreateTarefas(application, true);


            var tarefaTeste = new UpdateTarefaRequest { TarefaId = requestTarefaId, Titulo = requestTitulo, Descricao = requestDescricao, Status = requestStatus, DataConclusaoPrevista = new DateOnly(2025, 1, 2), DataConclusaoRealizada = new DateOnly(2025, 1, 1) };
            var jsonContent = JsonConvert.SerializeObject(tarefaTeste);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var client = application.CreateClient();
            var result = await client.PutAsync(url, contentString);
            var contents = await result.Content.ReadAsStringAsync();


            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            contents.Should().NotBeNull();
            contents.Should().Contain("A data de conclusão prevista tem que ser inferior ou igual a data de conclusão realizada.");
        }
    }
}
