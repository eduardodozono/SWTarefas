using FluentAssertions;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Usuarios;
using SWTarefas.Resources.Resources;
using SWTarefas.Tests.TestsMoq.Common.Cryptography;
using SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios;
using SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas;
using SWTarefas.Tests.TestsMoq.Common.Repositories.Usuarios;
using SWTarefas.Tests.TestsMoq.Common.Token;

namespace SWTarefas.Tests.TestsMoq.UsesCases.Usuarios.Write
{
    public class CreateLoginUsuariosUseCaseTestMoq
    {
        [Fact]
        public async Task Success()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);

            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build();

            var result = await CreateUseCase(usuarioReadRepository).Execute(createUsuariosLoginUseCaseRequest);

            result.Should().NotBeNull();
            result.AcessToken.Should().NotBeNullOrWhiteSpace();
            result.Email.Should().Be(createUsuariosLoginUseCaseRequest.Email);
        }

        [Fact]
        public async Task Error_Email_Ja_Cadastrado()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(true, true);

            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build();

            Func<Task> result = async () => await CreateUseCase(usuarioReadRepository).Execute(createUsuariosLoginUseCaseRequest);

            (await result.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.EmailJaExiste);
        }

        [Fact]
        public async Task Error_Email_Vazio()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);
            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Email_Vazio();

            Func<Task> result = async () => await CreateUseCase(usuarioReadRepository).Execute(createUsuariosLoginUseCaseRequest);

            (await result.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.EmailObrigatorio);
        }

        [Fact]
        public async Task Error_Email_Invalido()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);
            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Email_Invalido();

            Func<Task> result = async () => await CreateUseCase(usuarioReadRepository).Execute(createUsuariosLoginUseCaseRequest);

            (await result.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.EmailInvalido);
        }

        [Fact]
        public async Task Error_Senha_Vazia()
        {
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);
            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Senha_Vazia();

            Func<Task> result = async () => await CreateUseCase(usuarioReadRepository).Execute(createUsuariosLoginUseCaseRequest);

            (await result.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.PasswordObrigatorio);
        }

        [Fact]
        public async Task Error_Senha_Invalida()
        {
            var usuarioWriteRepository = UsuarioWriteRepositoryBuilder.Build();
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var customEncripter = CustomEncripterBuilder.Build();
            var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();

            var createLoginUsuariosUseCase = new CreateLoginUsuariosUseCase(usuarioWriteRepository, usuarioReadRepository, unitOfWork, customEncripter, jwtTokenGenerator);

            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Senha_Invalida();

            Func<Task> result = async () => await createLoginUsuariosUseCase.Execute(createUsuariosLoginUseCaseRequest);

            (await result.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.PasswordInvalido);
        }

        private static CreateLoginUsuariosUseCase CreateUseCase(IUsuarioReadRepository usuarioReadRepository)
        {
            var usuarioWriteRepository = UsuarioWriteRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var customEncripter = CustomEncripterBuilder.Build();
            var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();

            return new CreateLoginUsuariosUseCase(usuarioWriteRepository, usuarioReadRepository, unitOfWork, customEncripter, jwtTokenGenerator);
        }
    }
}
