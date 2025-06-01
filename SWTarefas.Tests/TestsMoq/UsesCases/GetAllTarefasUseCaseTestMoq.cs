using FluentAssertions;
using SWTarefas.Application.UsesCases.TarefasUseCases;
using SWTarefas.Tests.TestsMoq.Common.AutoMapper;
using SWTarefas.Tests.TestsMoq.Common.Entities;
using SWTarefas.Tests.TestsMoq.Common.Repositories;

namespace SWTarefas.Tests.TestsMoq.UsesCases
{
    public class GetAllTarefasUseCaseTestMoq
    {

        [Fact]
        public async Task Sucess()
        {
            var mapper = AutoMapperBuilder.Build();

            var listaTarefas = TarefasListBuilder.Build();

            var tarefa = TarefasBuilder.Build_Tarefa_Pendente();

            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, tarefa);

            var getAllTarefasUseCase = new GetAllTarefasUseCase(tarefaReadRepository, mapper);

            var result = await getAllTarefasUseCase.Execute();

            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(1);
        }

        [Fact]
        public async Task Error_Nenhuma_Tarefa_Encontrada()
        {
            var mapper = AutoMapperBuilder.Build();

            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(null, null);

            var getAllTarefasUseCase = new GetAllTarefasUseCase(tarefaReadRepository, mapper);

            var result = await getAllTarefasUseCase.Execute();

            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }
    }
}
