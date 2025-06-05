using FluentAssertions;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read;
using SWTarefas.Tests.TestsMoq.Common.AutoMapper;
using SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas;
using SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas;

namespace SWTarefas.Tests.TestsMoq.UsesCases.Tarefas.Read
{
    public class GetByIdTarefasUseCaseTestMoq
    {
        [Fact]
        public async Task Sucess()
        {
            var mapper = AutoMapperBuilder.Build();

            var listaTarefas = TarefasListBuilder.Build();

            var tarefa = TarefasBuilder.Build_Tarefa_Pendente();

            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, tarefa);

            var getByIdTarefasUseCase = new GetByIdTarefasUseCase(tarefaReadRepository, mapper);

            var result = await getByIdTarefasUseCase.Execute(1);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(tarefa.Titulo);
            result.Descricao.Should().Be(tarefa.Descricao);
            result.Status.Should().Be(tarefa.Status);
        }

        [Fact]
        public async Task Error_Tarefa_Nao_Existe()
        {
            var mapper = AutoMapperBuilder.Build();

            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(null, null);

            var getByIdTarefasUseCase = new GetByIdTarefasUseCase(tarefaReadRepository, mapper);

            var result = await getByIdTarefasUseCase.Execute(1);

            result.Should().BeNull();
        }
    }
}
