using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Write.Dapper
{
    public interface ICreateLoginUsuariosDapperUseCase
    {
        public Task<CreateUsuariosLoginUseCaseResponse> Execute(CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default);
    }
}
