using SWTarefas.Application.Cryptography;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Validations;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Usuarios;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases
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
            await Validate(request, token);

            var usuarioDomain = await _usuarioReadRepository.ExistsUsuarioByEmailAndPassword(request.Email, _customEncripter.Encrypt(request.Password), token);

            if (usuarioDomain == null)
                throw new Exception();
            // construir uma nova excetion
            // tratar essa execetion no filtro da api

            var tokenJwt = _jwtTokenGenerator.Generate(usuarioDomain.UsuarioIdentifier);

            return new UsuariosLoginUseCaseResponse(tokenJwt);
        }

        public async Task Validate(UsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var validation = new UsuarioLoginBaseValidation();

            var resultValidation = await validation.ValidateAsync(request, token);

            if (!resultValidation.IsValid)
                throw new CustomBadRequestException(resultValidation.Errors.Select(erros => erros.ErrorMessage).ToList());
            // construir uma nova excetion
            // tratar essa execetion no filtro da api
        }
    }
}
