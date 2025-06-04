using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;
using SWTarefas.Application.UsesCases.UsuariosUseCases.Interfaces;

namespace SWTarefas.Application.UsesCases.UsuariosUseCases
{
    public class LoginUsuariosUseCaseCreate: ILoginUsuariosUseCaseCreate
    {

        public async Task<UsuariosLoginUseCaseCreateResponse> Execute(UsuariosLoginUseCaseCreateRequest request, CancellationToken token = default)
        {
            // validacoes

            // inserir no banco

            //retorno

            return null;
        }

        public async Task Validate(UsuariosLoginUseCaseCreateRequest request, CancellationToken token = default)
        {

        }
    }
}
