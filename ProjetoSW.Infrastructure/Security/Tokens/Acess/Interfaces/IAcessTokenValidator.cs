namespace SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces
{
    public interface IAcessTokenValidator
    {
        public Guid ValidateAndGetUserIdentifier(string token);
    }
}
