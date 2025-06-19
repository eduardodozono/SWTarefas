using SWTarefas.Application.Cryptography;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Read.Dapper;
using SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Read.ValidatorBase;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Usuarios;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Read.Dapper
{
    public class LoginUsuariosDapperUseCase: ILoginUsuariosDapperUseCase
    {
        private readonly IUsuarioDapperReadRepository _usuarioDapperReadRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ICustomEncripter _customEncripter;

        public LoginUsuariosDapperUseCase(IUsuarioDapperReadRepository usuarioDapperReadRepository, IJwtTokenGenerator jwtTokenGenerator, ICustomEncripter customEncripter)
        {
            _usuarioDapperReadRepository = usuarioDapperReadRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _customEncripter = customEncripter;
        }

        public async Task<UsuariosLoginUseCaseResponse?> Execute(UsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            await UsuarioLoginValidator.Validate(request, token);

            var usuarioDomain = await _usuarioDapperReadRepository.ExistsUsuarioByEmailAndPassword(request.Email, _customEncripter.Encrypt(request.Password));

            if (usuarioDomain == null)
                throw new CustomUnauthorizedException(SWTarefasMessagesExceptions.UsuarioSenhaIncorretos);

            var tokenJwt = _jwtTokenGenerator.Generate(usuarioDomain.UsuarioIdentifier);

            return new UsuariosLoginUseCaseResponse(tokenJwt);
        }
    }
}
