using FluentAssertions;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases;
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
            var usuarioWriteRepository = UsuarioWriteRepositoryBuilder.Build();
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var customEncripter = CustomEncripterBuilder.Build();
            var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();

            var createLoginUsuariosUseCase = new CreateLoginUsuariosUseCase(usuarioWriteRepository, usuarioReadRepository, unitOfWork, customEncripter, jwtTokenGenerator);

            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build();

            var result = await createLoginUsuariosUseCase.Execute(createUsuariosLoginUseCaseRequest);

            result.Should().NotBeNull();
            result.AcessToken.Should().NotBeNullOrWhiteSpace();
            result.Email.Should().Be(createUsuariosLoginUseCaseRequest.Email);
        }

        [Fact]
        public async Task Error_Email_Ja_Cadastrado()
        {
            var usuarioWriteRepository = UsuarioWriteRepositoryBuilder.Build();
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(true, true);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var customEncripter = CustomEncripterBuilder.Build();
            var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();

            var createLoginUsuariosUseCase = new CreateLoginUsuariosUseCase(usuarioWriteRepository, usuarioReadRepository, unitOfWork, customEncripter, jwtTokenGenerator);

            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build();

            Func<Task> result = async () => await createLoginUsuariosUseCase.Execute(createUsuariosLoginUseCaseRequest);

            (await result.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.EmailJaExiste);
        }

        [Fact]
        public async Task Error_Email_Vazio()
        {
            var usuarioWriteRepository = UsuarioWriteRepositoryBuilder.Build();
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var customEncripter = CustomEncripterBuilder.Build();
            var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();

            var createLoginUsuariosUseCase = new CreateLoginUsuariosUseCase(usuarioWriteRepository, usuarioReadRepository, unitOfWork, customEncripter, jwtTokenGenerator);

            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Email_Vazio();

            Func<Task> result = async () => await createLoginUsuariosUseCase.Execute(createUsuariosLoginUseCaseRequest);

            (await result.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.EmailObrigatorio);
        }

        [Fact]
        public async Task Error_Email_Invalido()
        {
            var usuarioWriteRepository = UsuarioWriteRepositoryBuilder.Build();
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var customEncripter = CustomEncripterBuilder.Build();
            var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();

            var createLoginUsuariosUseCase = new CreateLoginUsuariosUseCase(usuarioWriteRepository, usuarioReadRepository, unitOfWork, customEncripter, jwtTokenGenerator);

            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Email_Invalido();

            Func<Task> result = async () => await createLoginUsuariosUseCase.Execute(createUsuariosLoginUseCaseRequest);

            (await result.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.EmailInvalido);
        }

        [Fact]
        public async Task Error_Senha_Vazia()
        {
            var usuarioWriteRepository = UsuarioWriteRepositoryBuilder.Build();
            var usuarioReadRepository = UsuarioReadRepositoryBuilder.Build(false, true);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var customEncripter = CustomEncripterBuilder.Build();
            var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();

            var createLoginUsuariosUseCase = new CreateLoginUsuariosUseCase(usuarioWriteRepository, usuarioReadRepository, unitOfWork, customEncripter, jwtTokenGenerator);

            var createUsuariosLoginUseCaseRequest = CreateUsuariosLoginUseCaseRequestBuilder.Build_Senha_Vazia();

            Func<Task> result = async () => await createLoginUsuariosUseCase.Execute(createUsuariosLoginUseCaseRequest);

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
    }
}
