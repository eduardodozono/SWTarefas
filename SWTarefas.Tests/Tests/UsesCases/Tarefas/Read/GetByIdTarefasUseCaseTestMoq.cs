using FluentAssertions;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;
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
            var listaTarefas = TarefasListBuilder.Build();
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente();
            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, tarefa);

            var result = await CreateUseCase(tarefaReadRepository).Execute(1);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(tarefa.Titulo);
            result.Descricao.Should().Be(tarefa.Descricao);
            result.Status.Should().Be(tarefa.Status);
        }

        [Fact]
        public async Task Error_Tarefa_Nao_Existe()
        {
            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(null, null);

            var result = await CreateUseCase(tarefaReadRepository).Execute(1);

            result.Should().BeNull();
        }

        private static GetByIdTarefasUseCase CreateUseCase(ITarefaReadRepository tarefaReadRepository)
        {
            var mapper = AutoMapperBuilder.Build();

            return new GetByIdTarefasUseCase(tarefaReadRepository, mapper);
        }
    }
}
