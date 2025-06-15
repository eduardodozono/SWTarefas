using Moq;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Usuarios;
using SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios;

namespace SWTarefas.Tests.TestsMoq.Common.Repositories.Usuarios
{
    public static class UsuarioReadRepositoryBuilder
    {
        public static IUsuarioReadRepository Build(bool existsEmail, bool existsUserIdentifier, bool adicionaUsuarioLista = true)
        {
            var usuarioReadRepository = new Mock<IUsuarioReadRepository>();

            var listaUsuario = UsuariosListBuilder.Build();

            var usuarioAdicionar = listaUsuario[0];

            if (!adicionaUsuarioLista)
                usuarioAdicionar = null;

            usuarioReadRepository.Setup(st => st.ExistsUsuarioByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(existsEmail);
            usuarioReadRepository.Setup(st => st.ExistsActiveUserWithIdentifier(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(existsUserIdentifier);
            usuarioReadRepository.Setup(st => st.ExistsUsuarioByEmailAndPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(usuarioAdicionar);

            return usuarioReadRepository.Object;
        }
    }
}
