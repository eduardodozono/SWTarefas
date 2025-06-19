using SWTarefas.Application.Cryptography;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Write.EF;
using SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Write.ValidatorBase;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Usuarios;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Write.EF
{
    public class CreateLoginUsuariosUseCase : ICreateLoginUsuariosUseCase
    {
        private readonly IUsuarioWriteRepository _usuarioWriteRepository;
        private readonly IUsuarioReadRepository _usuarioReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomEncripter _customEncripter;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public CreateLoginUsuariosUseCase(IUsuarioWriteRepository usuarioWriteRepository, IUsuarioReadRepository usuarioReadRepository,
            IUnitOfWork unitOfWork, ICustomEncripter customEncripter, IJwtTokenGenerator jwtTokenGenerator)
        {
            _usuarioWriteRepository = usuarioWriteRepository;
            _usuarioReadRepository = usuarioReadRepository;
            _unitOfWork = unitOfWork;
            _customEncripter = customEncripter;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<CreateUsuariosLoginUseCaseResponse> Execute(CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            await UsuarioLoginCreateValidator.Validate(request, token);

            var emailExists = await _usuarioReadRepository.ExistsUsuarioByEmail(request.Email);

            if (emailExists)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.EmailJaExiste);

            var usuarioDomain = new Usuario()
            {
                UsuarioIdentifier = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                Senha = _customEncripter.Encrypt(request.Password)
            };

            await _usuarioWriteRepository.Create(usuarioDomain, token);

            await _unitOfWork.Commit(token);

            var usuarioResponse = new CreateUsuariosLoginUseCaseResponse()
            {
                AcessToken = _jwtTokenGenerator.Generate(usuarioDomain.UsuarioIdentifier),
                RefreshToken = string.Empty,
                Email = usuarioDomain.Email,
                Nome = usuarioDomain.Nome
            };

            return usuarioResponse;
        }
    }
}
