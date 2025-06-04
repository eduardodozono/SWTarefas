using System.Security.Cryptography;
using System.Text;

namespace SWTarefas.Application.Cryptography
{
    public class CustomEncripter : ICustomEncripter
    {
        public async Task<string> EncryptAsync(string password)
        {
            string ret = string.Empty;

            await Task.Run(() =>
            {
                ret = Encrypt(password);
            });

            return ret;
        }

        public string Encrypt(string password)
        {
            string ret = string.Empty;

            var newPassword = $"{password}";

            var bytes = Encoding.UTF8.GetBytes(newPassword);

            var hashBytes = SHA512.HashData(bytes);

            ret = StringBytes(hashBytes);

            return ret;
        }

        private static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var data in bytes)
            {
                var hex = data.ToString("x2");

                sb.Append(hex);
            }

            return sb.ToString();
        }
    }
}
