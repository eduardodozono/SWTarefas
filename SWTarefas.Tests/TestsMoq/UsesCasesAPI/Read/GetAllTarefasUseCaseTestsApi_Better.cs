using SWTarefas.Tests.TestsMoq.Common.HostApi;
using System.Net;
using FluentAssertions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using Newtonsoft.Json;
using SWTarefas.Tests.TestsMoq.Common.Entities;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Tests.TestsMoq.UsesCasesAPI.Read
{
    public class GetAllTarefasUseCaseTestsApi_Better : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlEndPoint = "tarefas";
        private readonly CustomWebApplicationFactory _factory;

        public GetAllTarefasUseCaseTestsApi_Better(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            var listaTarefas = TarefasListBuilder.Build_Create();

            await TarefasDataBaseUtils.CreateTarefas(_factory, listaTarefas);

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

            var result = await _httpClient.GetAsync(_urlEndPoint);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<IEnumerable<GetAllTarefaResponse>?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            responseData.Should().BeNull(SWTarefasMessagesExceptions.TarefaNaoExiste);
        }
    }
}