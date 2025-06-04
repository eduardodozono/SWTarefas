namespace SWTarefas.Application.UsesCases.UsuariosUseCases.DTO
{
    public class UsuariosLoginUseCaseRequest : UsuariosUseCaseBase
    {
        public UsuariosLoginUseCaseRequest() { }

        public UsuariosLoginUseCaseRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
