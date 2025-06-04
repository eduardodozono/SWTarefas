namespace SWTarefas.Application.UsesCases.UsuariosUseCases.DTO
{
    public class CreateUsuariosLoginUseCaseRequest : UsuariosUseCaseBase
    {
        public string Nome { get; set; } = string.Empty;

        public CreateUsuariosLoginUseCaseRequest() { }

        public CreateUsuariosLoginUseCaseRequest(string nome, string email, string password)
        {
            Nome = nome;
            Email = email;
            Password = password;
        }
    }
}
