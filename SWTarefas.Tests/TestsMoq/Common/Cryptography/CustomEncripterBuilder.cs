using SWTarefas.Application.Cryptography;

namespace SWTarefas.Tests.TestsMoq.Common.Cryptography
{
    public static class CustomEncripterBuilder
    {
        public static ICustomEncripter Build()
        {
            return new CustomEncripter();           
        }
    }
}
