using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;
using SWTarefas.Resources.Resources;
using SWTarefas.Tests.TestsMoq.Common.AutoMapper;
using SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas;
using SWTarefas.Tests.TestsMoq.Common.HostApi;
using SWTarefas.Tests.TestsMoq.Common.UtilsApi;

namespace SWTarefas.Tests.TestsMoq.UsesCasesAPI.Tarefas.Write
{
    public class CreateTarefaUseCaseTestsApi_Better : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlEndPoint = "tarefas";
        private readonly CustomWebApplicationFactory _factory;

        public CreateTarefaUseCaseTestsApi_Better(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _factory = factory;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task Success_Create_Pendente_Concluido(int tarefaIndex)
        {
            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            var ListaTarefas = TarefasListBuilder.Build_Create();

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, ListaTarefas[tarefaIndex]);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<CreateTarefaResponse?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.Created);
            responseData.Should().NotBeNull();
            responseData.Titulo.Should().Be(ListaTarefas[tarefaIndex].Titulo);
            responseData.Descricao.Should().Be(ListaTarefas[tarefaIndex].Descricao);
            responseData.Status.Should().Be((int)TarefaStatus.Pendente);
        }

        [Fact]
        public async Task Error_Titulo_Vazio()
        {
            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            var mapper = AutoMapperBuilder.Build();

            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Titulo_Vazio();

            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().Contain(SWTarefasMessagesExceptions.TituloVazio);
        }

        [Fact]
        public async Task Error_Titulo_Max_Caracteres()
        {
            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            var mapper = AutoMapperBuilder.Build();

            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Titulo_Tamanho_Max();

            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().Contain(SWTarefasMessagesExceptions.TItuloMaximoCaracteres);
        }

        [Fact]
        public async Task Error_Descricao_Max_Caracteres()
        {
            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            var mapper = AutoMapperBuilder.Build();

            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Descricao_Tamanho_Max();

            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().Contain(SWTarefasMessagesExceptions.DescricaoMaximoCaracteres);
        }

        [Fact]
        public async Task Error_Data_Prevista_Vazia()
        {
            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            var mapper = AutoMapperBuilder.Build();

            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Data_Prevista_Vazia();

            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, tarefaRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().Contain(SWTarefasMessagesExceptions.ErroDataPrevistaVazia);
        }
    }
}
