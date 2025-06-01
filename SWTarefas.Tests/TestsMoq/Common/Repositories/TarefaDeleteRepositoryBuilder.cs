using Moq;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;
using SWTarefas.Tests.TestsMoq.UsesCases;

namespace SWTarefas.Tests.TestsMoq.Common.Repositories
{
    public static class TarefaDeleteRepositoryBuilder
    {
        public static ITarefaDeleteRepository Build()
        {           
            var tarefaDeleteRepository = new Mock<ITarefaDeleteRepository>();

            tarefaDeleteRepository.Setup(st=> st.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>()));

            return tarefaDeleteRepository.Object;
        }
    }
}
