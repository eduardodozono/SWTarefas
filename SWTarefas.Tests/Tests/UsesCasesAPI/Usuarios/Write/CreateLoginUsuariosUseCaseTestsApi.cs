using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using System.Net.Http.Json;
using SWTarefas.Tests.TestsMoq.Common.HostApi;
using SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios;
using FluentAssertions;
using System.Net;
using Newtonsoft.Json;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Tests.TestsMoq.UsesCasesAPI.Usuarios.Write
{
    public class CreateLoginUsuariosUseCaseTestsApi : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlEndPoint = "usuarios";
        private readonly CustomWebApplicationFactory _factory;

        public CreateLoginUsuariosUseCaseTestsApi(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build();

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, createUsuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<CreateUsuariosLoginUseCaseResponse>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.Created);
            responseData.Should().NotBeNull();
            responseData.AcessToken.Should().NotBeNullOrWhiteSpace();
            responseData.Email.Should().Be(createUsuariosLoginUseCaseRequest.Email);
        }

        [Fact]
        public async Task Error_Senha_Vazia()
        {
            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Senha_Vazia();

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, createUsuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.PasswordObrigatorio);
        }

        [Fact]
        public async Task Error_Senha_Invalida()
        {
            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Senha_Invalida();

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, createUsuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.PasswordInvalido);
        }

        [Fact]
        public async Task Error_Email_Vazio()
        {
            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Email_Vazio();

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, createUsuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.EmailObrigatorio);
        }

        [Fact]
        public async Task Error_Email_Invalido()
        {
            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Email_Invalido();

            var result = await _httpClient.PostAsJsonAsync(_urlEndPoint, createUsuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            responseBody.Should().Contain(SWTarefasMessagesExceptions.EmailInvalido);
        }
    }
}
