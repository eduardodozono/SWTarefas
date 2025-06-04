using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces
{
    public interface ILoginUsuariosUseCaseCreate
    {
        public Task<UsuariosLoginUseCaseCreateResponse> Execute(UsuariosLoginUseCaseCreateRequest request, CancellationToken token = default);

        public Task Validate(UsuariosLoginUseCaseCreateRequest request, CancellationToken token = default);
    }
}
