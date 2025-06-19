using SWTarefas.Application.Cryptography;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Read.EF;
using SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Read.ValidatorBase;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Usuarios;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Read.EF
{
    public class LoginUsuariosUseCase : ILoginUsuariosUseCase
    {
        private readonly IUsuarioReadRepository _usuarioReadRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ICustomEncripter _customEncripter;

        public LoginUsuariosUseCase(IUsuarioReadRepository usuarioReadRepository, IJwtTokenGenerator jwtTokenGenerator, ICustomEncripter customEncripter)
        {
            _usuarioReadRepository = usuarioReadRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _customEncripter = customEncripter;
        }

        public async Task<UsuariosLoginUseCaseResponse?> Execute(UsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            await UsuarioLoginValidator.Validate(request, token);

            var usuarioDomain = await _usuarioReadRepository.ExistsUsuarioByEmailAndPassword(request.Email, _customEncripter.Encrypt(request.Password), token);

            if (usuarioDomain == null)
                throw new CustomUnauthorizedException(SWTarefasMessagesExceptions.UsuarioSenhaIncorretos);
            // construir uma nova excetion
            // tratar essa execetion no filtro da api

            var tokenJwt = _jwtTokenGenerator.Generate(usuarioDomain.UsuarioIdentifier);

            return new UsuariosLoginUseCaseResponse(tokenJwt);
        }
    }
}
