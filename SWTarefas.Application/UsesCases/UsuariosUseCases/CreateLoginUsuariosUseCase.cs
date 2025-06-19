using SWTarefas.Application.Cryptography;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Validations;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.UnitOfWork;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Usuarios;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases
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
            await Validate(request, token);

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

        public async Task Validate(CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            var validation = new UsuarioLoginBaseValidation();

            var result = await validation.ValidateAsync(request);

            var emailExists = await _usuarioReadRepository.ExistsUsuarioByEmail(request.Email);

            if (emailExists)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, SWTarefasMessagesExceptions.EmailJaExiste));

            if (!result.IsValid)
                throw new CustomBadRequestException(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
