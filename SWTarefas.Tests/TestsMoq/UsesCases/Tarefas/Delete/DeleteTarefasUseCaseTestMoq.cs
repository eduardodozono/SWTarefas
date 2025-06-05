using FluentAssertions;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Delete;
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

            var unitOfWork = UnitOfWorkBuilder.Build();
            var tarefaDeleteRepository = TarefaDeleteRepositoryBuilder.Build();
            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, tarefa);

            var deleteTarefasUseCase = new DeleteTarefasUseCase(tarefaDeleteRepository, tarefaReadRepository, unitOfWork);

            var result = await deleteTarefasUseCase.Execute(1);

            result.Should().Be(1);
        }

        [Fact]
        public async Task Error_Tarefa_Nao_Existe()
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var tarefaDeleteRepository = TarefaDeleteRepositoryBuilder.Build();
            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(null, null);

            var deleteTarefasUseCase = new DeleteTarefasUseCase(tarefaDeleteRepository, tarefaReadRepository, unitOfWork);

            Func<Task> act = async () => await deleteTarefasUseCase.Execute(1);

            await act.Should().ThrowAsync<CustomBadRequestException>();
        }
    }
}