namespace SWTarefas.Application.Cryptography
{
    public interface ICustomEncripter
    {
        public  Task<string> EncryptAsync(string password);
        public string Encrypt(string password);
    }
}
