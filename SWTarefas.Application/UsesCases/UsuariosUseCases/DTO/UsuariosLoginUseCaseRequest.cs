namespace SWTarefas.Application.UsesCases.UsuariosUseCases.DTO
{
    public class UsuariosLoginUseCaseRequest
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;

        public UsuariosLoginUseCaseRequest() { }

        public UsuariosLoginUseCaseRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }       
    }
}
