using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Read.Dapper
{
    public interface ILoginUsuariosDapperUseCase
    {
        public Task<UsuariosLoginUseCaseResponse?> Execute(UsuariosLoginUseCaseRequest request, CancellationToken token = default);
    }
}
