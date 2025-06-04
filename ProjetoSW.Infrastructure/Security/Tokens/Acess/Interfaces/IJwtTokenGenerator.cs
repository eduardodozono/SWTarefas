namespace SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string Generate(Guid userIdentifier);
    }
}
