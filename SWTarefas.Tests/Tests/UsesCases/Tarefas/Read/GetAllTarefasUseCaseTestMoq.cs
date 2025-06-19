using FluentAssertions;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read;
using SWTarefas.Tests.TestsMoq.Common.AutoMapper;
using SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas;
using SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas;

namespace SWTarefas.Tests.TestsMoq.UsesCases.Tarefas.Read
{
    public class GetAllTarefasUseCaseTestMoq
    {
        //[Fact]
        //public async Task Sucess()
        //{
        //    var listaTarefas = TarefasListBuilder.Build();
        //    var tarefa = TarefasBuilder.Build_Tarefa_Pendente();
        //    var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, tarefa);

        //    var result = await CreateUseCase(tarefaReadRepository).Execute();

        //    result.Should().NotBeNull();
        //    result.Should().HaveCountGreaterThan(1);
        //}

        //[Fact]
        //public async Task Error_Nenhuma_Tarefa_Encontrada()
        //{
        //    var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(null, null);

        //    var result = await CreateUseCase(tarefaReadRepository).Execute();

        //    result.Should().NotBeNull();
        //    result.Should().HaveCount(0);
        //}

        //private static GetAllTarefasUseCase CreateUseCase(ITarefaReadRepository tarefaReadRepository)
        //{
        //    var mapper = AutoMapperBuilder.Build();

        //    return new GetAllTarefasUseCase(tarefaReadRepository, mapper);
        //}
    }
}
