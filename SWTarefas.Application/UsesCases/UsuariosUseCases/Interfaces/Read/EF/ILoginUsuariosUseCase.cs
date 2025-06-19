using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Read.EF
{
    public interface ILoginUsuariosUseCase
    {
        public Task<UsuariosLoginUseCaseResponse?> Execute(UsuariosLoginUseCaseRequest request, CancellationToken  token = default);
    }
}
