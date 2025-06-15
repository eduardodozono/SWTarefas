using SWTarefas.Tests.TestsMoq.Common.HostApi;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using SWTarefas.Tests.TestsMoq.Common.UtilsApi;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Response;

namespace SWTarefas.Tests.TestsMoq.UsesCasesAPI.Tarefas.Read
{
    public class GetByIdTarefasUseCaseTestsApi : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlEndPoint = "tarefas";
        private readonly CustomWebApplicationFactory _factory;

        public GetByIdTarefasUseCaseTestsApi(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task Success()
        {
            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            var listaTarefas = await UtilsApi.Start_TarefasList(_factory);

            string urlIdEndPoint = $"/{_urlEndPoint}/{listaTarefas[0].TarefaId}";

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.GetAsync(urlIdEndPoint);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<GetByIdTarefaResponse?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            responseData.Should().NotBeNull();
        }

        [Fact]
        public async Task Success_No_Content()
        {
            string urlIdEndPoint = $"/{_urlEndPoint}/0";

            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            var listaTarefas = await UtilsApi.Start_TarefasList(_factory);

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.GetAsync(urlIdEndPoint);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<GetByIdTarefaResponse?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            responseData.Should().BeNull();
        }
    }
}
