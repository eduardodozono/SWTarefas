using Moq;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Usuarios;
using SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios;

namespace SWTarefas.Tests.TestsMoq.Common.Repositories.Usuarios
{
    public static class UsuarioWriteRepositoryBuilder
    {
        public static IUsuarioWriteRepository Build()
        {
            var listaUsuario = UsuariosListBuilder.Build();

            var usuarioWriteRepository = new Mock<IUsuarioWriteRepository>();

            usuarioWriteRepository.Setup(st => st.Create(It.IsAny<Usuario>(), It.IsAny<CancellationToken>())).ReturnsAsync(listaUsuario[0]);

            return usuarioWriteRepository.Object;
        }
    }
}
