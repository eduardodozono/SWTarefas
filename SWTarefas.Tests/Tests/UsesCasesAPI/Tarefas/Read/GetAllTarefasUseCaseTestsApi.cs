using SWTarefas.Tests.TestsMoq.Common.HostApi;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using SWTarefas.Resources.Resources;
using SWTarefas.Tests.TestsMoq.Common.UtilsApi;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Tests.TestsMoq.UsesCasesAPI.Tarefas.Read
{
    public class GetAllTarefasUseCaseTestsApi : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlEndPoint = "tarefas";
        private readonly CustomWebApplicationFactory _factory;

        public GetAllTarefasUseCaseTestsApi(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            var listaTarefas = await UtilsApi.Start_TarefasList(_factory);

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.GetAsync(_urlEndPoint);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<IEnumerable<GetAllTarefaResponse>?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            responseData.Should().NotBeNullOrEmpty();
            responseData.Should().HaveCountGreaterThan(1);
        }

        [Fact]
        public async Task Success_No_Content()
        {
            await TarefasDataBaseUtils.DeleteAllTarefas(_factory);

            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.GetAsync(_urlEndPoint);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<IEnumerable<GetAllTarefaResponse>?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            responseData.Should().BeNull(SWTarefasMessagesExceptions.TarefaNaoExiste);
        }
    }
}