using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Resources.Resources;
using SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios;
using SWTarefas.Tests.TestsMoq.Common.HostApi;
using SWTarefas.Tests.TestsMoq.Common.UtilsApi;

namespace SWTarefas.Tests.TestsMoq.UsesCasesAPI.Usuarios.Read
{
    public class LoginUsuariosUseCaseTestsApi : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlEndPoint = "usuarios";
        private readonly string _urlEndPointLogin = "usuarios/login";
        private readonly CustomWebApplicationFactory _factory;

        public LoginUsuariosUseCaseTestsApi(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Sucess()
        {
            string senhaGuid = Guid.NewGuid().ToString();

            var listaUsuarios = await UtilsApi.Start_UsuarioList_Encrypt(_factory, senhaGuid);

            var usuario = listaUsuarios[0];

            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build_Custom(usuario.Email, senhaGuid);

            var result = await _httpClient.PostAsJsonAsync(_urlEndPointLogin, usuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<UsuariosLoginUseCaseResponse?>(responseBody);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            responseData.Should().NotBeNull();
            responseData.AcessToken.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Senha_Incorreta()
        {
            string senhaGuid = Guid.NewGuid().ToString();

            var listaUsuarios = await UtilsApi.Start_UsuarioList_Encrypt(_factory, senhaGuid);

            var usuario = listaUsuarios[0];

            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build_Custom(usuario.Email, Guid.NewGuid().ToString());

            var result = await _httpClient.PostAsJsonAsync(_urlEndPointLogin, usuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            responseBody.Should().Contain(SWTarefasMessagesExceptions.UsuarioSenhaIncorretos);
        }

        [Fact]
        public async Task Error_Senha_Vazia()
        {
            string senhaGuid = Guid.NewGuid().ToString();

            var listaUsuarios = await UtilsApi.Start_UsuarioList_Encrypt(_factory, senhaGuid);

            var usuario = listaUsuarios[0];

            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build_Custom(usuario.Email, string.Empty);

            var result = await _httpClient.PostAsJsonAsync(_urlEndPointLogin, usuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().Contain(SWTarefasMessagesExceptions.PasswordObrigatorio);
        }

        [Fact]
        public async Task Error_Email_Vazio()
        {
            string senhaGuid = Guid.NewGuid().ToString();

            var listaUsuarios = await UtilsApi.Start_UsuarioList_Encrypt(_factory, senhaGuid);

            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build_Custom(string.Empty, senhaGuid);

            var result = await _httpClient.PostAsJsonAsync(_urlEndPointLogin, usuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().Contain(SWTarefasMessagesExceptions.EmailObrigatorio);
        }

        [Fact]
        public async Task Error_Email_Invalido()
        {
            string senhaGuid = Guid.NewGuid().ToString();

            var listaUsuarios = await UtilsApi.Start_UsuarioList_Encrypt(_factory, senhaGuid);

            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build_Custom(Guid.NewGuid().ToString(), senhaGuid);

            var result = await _httpClient.PostAsJsonAsync(_urlEndPointLogin, usuariosLoginUseCaseRequest);

            var responseBody = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().Contain(SWTarefasMessagesExceptions.EmailInvalido);
        }
    }
}
