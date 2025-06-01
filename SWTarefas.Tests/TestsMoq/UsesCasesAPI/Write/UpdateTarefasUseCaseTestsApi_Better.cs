using Newtonsoft.Json;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Tests.TestsMoq.Common.Entities;
using System.Net.Http.Json;
using System.Net;
using SWTarefas.Tests.TestsMoq.Common.HostApi;
using FluentAssertions;
using SWTarefas.Tests.TestsMoq.Common.AutoMapper;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Tests.TestsMoq.UsesCasesAPI.Write
{
    public class UpdateTarefasUseCaseTestsApi_Better : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlEndPoint = "tarefas";
        private readonly CustomWebApplicationFactory _factory;

        public UpdateTarefasUseCaseTestsApi_Better(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task Sucess()
        {
            var ListaTarefas = TarefasListBuilder.Build();

            await TarefasDataBaseUtils.DeleteAllTarefas(_factory);

            await TarefasDataBaseUtils.CreateTarefas(_factory, ListaTarefas);

            var mapper = AutoMapperBuilder.Build();

            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(ListaTarefas[0]);

            var result = await _httpClient.PutAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<UpdateTarefaResponse?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            responseData.Should().NotBeNull();
            responseData.Titulo.Should().Be(ListaTarefas[0].Titulo);
            responseData.Descricao.Should().Be(ListaTarefas[0].Descricao);
        }

        [Fact]
        public async Task Error_Titulo_Vazio()
        {
            var ListaTarefas = TarefasListBuilder.Build();

            await TarefasDataBaseUtils.DeleteAllTarefas(_factory);

            await TarefasDataBaseUtils.CreateTarefas(_factory, ListaTarefas);

            var mapper = AutoMapperBuilder.Build();

            var tarefaUpdate = TarefasBuilder.Build_Tarefa_Pendente_Titulo_Vazio();

            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefaUpdate);

            var result = await _httpClient.PutAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();           

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.TituloVazio);
        }

        [Fact]
        public async Task Error_Titulo_Max_Caracteres()
        {
            var ListaTarefas = TarefasListBuilder.Build();

            await TarefasDataBaseUtils.DeleteAllTarefas(_factory);

            await TarefasDataBaseUtils.CreateTarefas(_factory, ListaTarefas);

            var mapper = AutoMapperBuilder.Build();

            var tarefaUpdate = TarefasBuilder.Build_Tarefa_Pendente_Titulo_Tamanho_Max();

            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefaUpdate);

            var result = await _httpClient.PutAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.TItuloMaximoCaracteres);
        }

        [Fact]
        public async Task Error_Descricao_Max_Caracteres()
        {
            var ListaTarefas = TarefasListBuilder.Build();

            await TarefasDataBaseUtils.DeleteAllTarefas(_factory);

            await TarefasDataBaseUtils.CreateTarefas(_factory, ListaTarefas);

            var mapper = AutoMapperBuilder.Build();

            var tarefaUpdate = TarefasBuilder.Build_Tarefa_Pendente_Descricao_Tamanho_Max();

            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefaUpdate);

            var result = await _httpClient.PutAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.DescricaoMaximoCaracteres);
        }

        [Fact]
        public async Task Error_Data_Prevista_Vazia()
        {
            var ListaTarefas = TarefasListBuilder.Build();

            await TarefasDataBaseUtils.DeleteAllTarefas(_factory);

            await TarefasDataBaseUtils.CreateTarefas(_factory, ListaTarefas);

            var mapper = AutoMapperBuilder.Build();

            var tarefaUpdate = TarefasBuilder.Build_Tarefa_Pendente_Data_Prevista_Vazia();

            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefaUpdate);

            var result = await _httpClient.PutAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.ErroDataPrevistaVazia);
        }

        [Fact]
        public async Task Error_Status_Vazio()
        {
            var ListaTarefas = TarefasListBuilder.Build();

            await TarefasDataBaseUtils.DeleteAllTarefas(_factory);

            await TarefasDataBaseUtils.CreateTarefas(_factory, ListaTarefas);

            var mapper = AutoMapperBuilder.Build();

            var tarefaUpdate = TarefasBuilder.Build_Tarefa_Pendente_Status_Vazio();

            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefaUpdate);

            var result = await _httpClient.PutAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.StatusVazio);
        }

        [Fact]
        public async Task Error_Status_Invalido()
        {
            var ListaTarefas = TarefasListBuilder.Build();

            await TarefasDataBaseUtils.DeleteAllTarefas(_factory);

            await TarefasDataBaseUtils.CreateTarefas(_factory, ListaTarefas);

            var mapper = AutoMapperBuilder.Build();

            var tarefaUpdate = TarefasBuilder.Build_Tarefa_Pendente_Status_Invalido();

            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefaUpdate);

            var result = await _httpClient.PutAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.StatusInvalido);
        }
    }
}
