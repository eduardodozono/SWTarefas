using Newtonsoft.Json;
using static SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums.StatusEnum;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using System.Net;
using System.Text;
using FluentAssertions;

namespace SWTarefas.Tests.Tests.TarefasApi.Update
{
    public class TarefasApiTestsUpdate
    {
        private const string url = "/tarefas";

        [Theory]
        [InlineData(1, (int)TarefaStatus.Pendente, "Titulo 1", "Decricao 1")]
        [InlineData(2, (int)TarefaStatus.Concluída, "Titulo 2", "Decricao 2")]
        public async Task TarefasAPI_Update_OK(int requestTarefaId, int requestStatus, string requestTitulo, string requestDescricao)
        {
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
        [InlineData(99, (int)TarefaStatus.Pendente, "Titulo 99", "Decricao 99")]
        [InlineData(100, (int)TarefaStatus.Concluída, "Titulo 100", "Decricao 100")]
        public async Task TarefasAPI_Update_Tarefa_Nao_Encontrada_BadRequest(int requestTarefaId, int requestStatus, string requestTitulo, string requestDescricao)
        {
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
        [InlineData(1, 3, "Titulo Status Invalido 1", "Decricao Status Invalido 1")]
        [InlineData(2, 4, "Titulo Status Invalido 2", "Decricao Status Invalido 2")]
        public async Task TarefasAPI_Update_Status_Invalido_BadRequest(int requestTarefaId, int requestStatus, string requestTitulo, string requestDescricao)
        {
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
        [InlineData(1, 1, "", "Decricao Titulo Invalido 1")]
        [InlineData(2, 2, "", "Decricao Titulo Invalido 2")]
        public async Task TarefasAPI_Update_Titulo_Vazio_Invalido_BadRequest(int requestTarefaId, int requestStatus, string requestTitulo, string requestDescricao)
        {
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
            contents.Should().Contain("O campo titulo não pode fica vazio.");
        }


        [Theory]
        [InlineData(1, 1, "Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido  Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido", "Decricao Titulo Invalido 1")]
        [InlineData(2, 2, "Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido  Titulo Maximo Caracteres Invalido Titulo Maximo Caracteres Invalido", "")]
        public async Task TarefasAPI_Update_Titulo_Maximo_Caracteres_Invalido_BadRequest(int requestTarefaId, int requestStatus, string requestTitulo, string requestDescricao)
        {
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
            contents.Should().Contain("O campo titulo tem no máximo 100 cacteres.");
        }


        [Theory]
        [InlineData(1, 1, "Titulo Datas Invalidas 1", "Decricao Datas Invalidas 1")]
        [InlineData(2, 2, "Titulo Datas Invalidas 2", "Decricao Datas Invalidas 2")]
        public async Task TarefasAPI_Update_DataPrevista_DataRealizada_Invalida_BadRequest(int requestTarefaId, int requestStatus, string requestTitulo, string requestDescricao)
        {
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
