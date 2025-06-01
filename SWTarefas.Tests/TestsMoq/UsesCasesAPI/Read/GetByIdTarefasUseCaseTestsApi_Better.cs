using SWTarefas.Tests.TestsMoq.Common.HostApi;
using System.Net;
using FluentAssertions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using Newtonsoft.Json;
using SWTarefas.Tests.TestsMoq.Common.Entities;

namespace SWTarefas.Tests.TestsMoq.UsesCasesAPI.Read
{
    public class GetByIdTarefasUseCaseTestsApi_Better : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlEndPoint = "tarefas";
        private readonly CustomWebApplicationFactory _factory;

        public GetByIdTarefasUseCaseTestsApi_Better(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _factory = factory;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Success(int tarefaId)
        {
            var listaTarefas = TarefasListBuilder.Build_Create();

            await TarefasDataBaseUtils.CreateTarefas(_factory, listaTarefas);

            string urlIdEndPoint = $"/{_urlEndPoint}/{tarefaId}";

            var result = await _httpClient.GetAsync(urlIdEndPoint);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<GetByIdTarefaResponse?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            responseData.Should().NotBeNull();
        }

        [Fact]
        public async Task Success_No_Content()
        {
            var listaTarefas = TarefasListBuilder.Build_Create();

            await TarefasDataBaseUtils.CreateTarefas(_factory, listaTarefas);

            string urlIdEndPoint = $"/{_urlEndPoint}/0";

            var result = await _httpClient.GetAsync(urlIdEndPoint);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<GetByIdTarefaResponse?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            responseData.Should().BeNull();
        }
    }
}
