namespace SWTarefas.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; init; }
        public Guid UsuarioIdentifier { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string Senha { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;

        public Usuario() { }

        public Usuario(int usuarioId, Guid usuarioIdentifier, string nome, string senha, string email)
        {
            UsuarioId = usuarioId;
            UsuarioIdentifier = usuarioIdentifier;
            Nome = nome;
            Senha = senha;
            Email = email;
        }
    }
}
