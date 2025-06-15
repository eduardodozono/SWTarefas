using FluentAssertions;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Usuarios;
using SWTarefas.Resources.Resources;
using SWTarefas.Tests.TestsMoq.Common.Cryptography;
using SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios;
using SWTarefas.Tests.TestsMoq.Common.Repositories.Usuarios;
using SWTarefas.Tests.TestsMoq.Common.Token;

namespace SWTarefas.Tests.TestsMoq.UsesCases.Usuarios.Read
{
    public class LoginUsuariosUseCaseTestMoq
    {
        [Fact]
        public async Task Sucess()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);
            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build();

            var result = await CreateUseCase(usuarioReadRepository).Execute(usuariosLoginUseCaseRequest);

            result.Should().NotBeNull();
            result.AcessToken.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Email_Nao_Encontrado()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, false, false);
            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build();

            Func<Task> act = async () => await CreateUseCase(usuarioReadRepository).Execute(usuariosLoginUseCaseRequest);

            (await act.Should().ThrowAsync<CustomUnauthorizedException>()).And._errors.Contains(SWTarefasMessagesExceptions.UsuarioSenhaIncorretos);
        }

        [Fact]
        public async Task Error_Email_Vazio()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, false);
            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build_Email_Vazio();

            Func<Task> act = async () => await CreateUseCase(usuarioReadRepository).Execute(usuariosLoginUseCaseRequest);

            (await act.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.EmailObrigatorio);
        }

        [Fact]
        public async Task Error_Email_Invalido()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, false);
            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build_Email_Vazio();

            Func<Task> act = async () => await CreateUseCase(usuarioReadRepository).Execute(usuariosLoginUseCaseRequest);

            (await act.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.EmailInvalido);
        }

        [Fact]
        public async Task Error_Senha_Vazia()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, false);
            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build_Senha_Vazia();

            Func<Task> act = async () => await CreateUseCase(usuarioReadRepository).Execute(usuariosLoginUseCaseRequest);

            (await act.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.PasswordObrigatorio);
        }

        [Fact]
        public async Task Error_Senha_Invalida()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, false);
            var usuariosLoginUseCaseRequest = UsuariosLoginUseCaseRequestBuilder.Build_Senha_Invalida();

            Func<Task> act = async () => await CreateUseCase(usuarioReadRepository).Execute(usuariosLoginUseCaseRequest);

            (await act.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.PasswordInvalido);
        }

        private static LoginUsuariosUseCase CreateUseCase(IUsuarioReadRepository usuarioReadRepository)
        {
            var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();
            var customEncripter = CustomEncripterBuilder.Build();

            return new LoginUsuariosUseCase(usuarioReadRepository, jwtTokenGenerator, customEncripter);
        }
    }
}
