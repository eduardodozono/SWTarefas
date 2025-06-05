using System.Net;
using SWTarefas.Tests.TestsMoq.Common.HostApi;
using FluentAssertions;
using SWTarefas.Resources.Resources;
using SWTarefas.Tests.TestsMoq.Common.UtilsApi;

namespace SWTarefas.Tests.TestsMoq.UsesCasesAPI.Tarefas.Delete
{
    public class DeleteTarefasUseCaseTestsApi_Better : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlEndPoint = "tarefas";
        private readonly CustomWebApplicationFactory _factory;

        public DeleteTarefasUseCaseTestsApi_Better(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task Sucess()
        {
            var deleteUrlEndPoint = $"/{_urlEndPoint}/1";

            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            await UtilsApi.Start_TarefasList(_factory);

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.DeleteAsync(deleteUrlEndPoint);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Tarefa_Nao_Encontrada()
        {
            var deleteUrlEndPoint = $"/{_urlEndPoint}/0";

            var listaUsuarios = await UtilsApi.Start_UsuarioList(_factory);

            await UtilsApi.Start_TarefasList(_factory);

            UtilsApi.AddTokenHeader(_httpClient, listaUsuarios[0]);

            var result = await _httpClient.DeleteAsync(deleteUrlEndPoint);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().Contain(SWTarefasMessagesExceptions.TarefaNaoExiste);
        }
    }
}
