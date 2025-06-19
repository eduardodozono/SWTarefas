using Moq;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;

namespace SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas
{
    public static class TarefaWriteRepositoryBuilder
    {
        public static ITarefaWriteRepository Build(Tarefa tarefa)
        {
            var tarefaWriteRepository = new Mock<ITarefaWriteRepository>();

            tarefaWriteRepository.Setup(st=> st.Create(tarefa, It.IsAny<CancellationToken>())).ReturnsAsync(tarefa);

            tarefaWriteRepository.Setup(st => st.Update(tarefa)).Returns(tarefa);

            return tarefaWriteRepository.Object;
        }
    }
}
