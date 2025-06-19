using SWTarefas.Application.Cryptography;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Write.Dapper;
using SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Write.ValidatorBase;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.UnitOfWork;
using SWTarefas.Infrastructure.DataAcess.Dapper.Interfaces.Usuarios;
using SWTarefas.Infrastructure.DataAcess.Dapper.RepositoryDapper.Usuarios;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;
using SWTarefas.Resources.Resources;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.UseCases.Write.Dapper
{
    public class CreateLoginUsuariosDapperUseCase : ICreateLoginUsuariosDapperUseCase
    {
        private readonly IUsuarioDapperWriteRepository _usuarioDapperWriteRepository;
        private readonly IUsuarioDapperReadRepository _usuarioDapperReadRepository;
        private readonly ICustomEncripter _customEncripter;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUnitOfWorkDapper _unitOfWorkDapper;

        public CreateLoginUsuariosDapperUseCase(IUsuarioDapperWriteRepository usuarioDapperWriteRepository, IUsuarioDapperReadRepository usuarioDapperReadRepository, ICustomEncripter customEncripter, IJwtTokenGenerator jwtTokenGenerator, IUnitOfWorkDapper unitOfWorkDapper)
        {
            _usuarioDapperWriteRepository = usuarioDapperWriteRepository;
            _usuarioDapperReadRepository = usuarioDapperReadRepository;
            _customEncripter = customEncripter;
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWorkDapper = unitOfWorkDapper;
        }

        public async Task<CreateUsuariosLoginUseCaseResponse> Execute(CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default)
        {
            await UsuarioLoginCreateValidator.Validate(request, token);

            var emailExists = await _usuarioDapperReadRepository.ExistsUsuarioByEmail(request.Email);

            if (emailExists)
                throw new CustomBadRequestException(SWTarefasMessagesExceptions.EmailJaExiste);

            var usuarioDomain = new Usuario()
            {
                UsuarioIdentifier = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                Senha = _customEncripter.Encrypt(request.Password)
            };

            _unitOfWorkDapper.BeginTransaction();

            await _usuarioDapperWriteRepository.Create(usuarioDomain);

            _unitOfWorkDapper.Commit();

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
