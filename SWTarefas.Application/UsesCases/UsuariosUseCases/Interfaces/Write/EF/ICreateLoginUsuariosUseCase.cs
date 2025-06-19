using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces.Write.EF
{
    public interface ICreateLoginUsuariosUseCase
    {
        public Task<CreateUsuariosLoginUseCaseResponse> Execute(CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default);
    }
}
