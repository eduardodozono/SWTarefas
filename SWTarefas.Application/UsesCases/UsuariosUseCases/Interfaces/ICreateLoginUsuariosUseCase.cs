using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces
{
    public interface ICreateLoginUsuariosUseCase
    {
        public Task<CreateUsuariosLoginUseCaseResponse> Execute(CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default);

        public Task Validate(CreateUsuariosLoginUseCaseRequest request, CancellationToken token = default);
    }
}
