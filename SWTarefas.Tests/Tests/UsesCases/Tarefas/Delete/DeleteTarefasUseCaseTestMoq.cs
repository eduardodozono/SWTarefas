using FluentAssertions;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Delete.EF;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;
using SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas;
using SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas;

namespace SWTarefas.Tests.TestsMoq.UsesCases.Tarefas.Delete
{
    public class DeleteTarefasUseCaseTestMoq
    {
        [Fact]
        public async Task Sucess()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente();
            var listaTarefas = TarefasListBuilder.Build();
            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, tarefa);

            var result = await CreateUseCase(tarefaReadRepository).Execute(1);

            result.Should().Be(1);
        }

        [Fact]
        public async Task Error_Tarefa_Nao_Existe()
        {
            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(null, null);

            Func<Task> act = async () => await CreateUseCase(tarefaReadRepository).Execute(1);

            await act.Should().ThrowAsync<CustomBadRequestException>();
        }

        private static DeleteTarefasUseCase CreateUseCase(ITarefaReadRepository tarefaReadRepository)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var tarefaDeleteRepository = TarefaDeleteRepositoryBuilder.Build();

            return new DeleteTarefasUseCase(tarefaDeleteRepository, tarefaReadRepository, unitOfWork);
        }
    }
}