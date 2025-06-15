using Moq;
using SWTarefas.Infrastructure.DataAcess.Interfaces.UnitOfWork;

namespace SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas
{
    public static class UnitOfWorkBuilder
    {
        public static IUnitOfWork Build()
        {
            var unitOfWork = new Mock<IUnitOfWork>();

            return unitOfWork.Object;
        }
    }
}
