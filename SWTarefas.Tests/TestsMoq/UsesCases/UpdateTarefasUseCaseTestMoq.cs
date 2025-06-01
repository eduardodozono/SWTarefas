using FluentAssertions;
using SWTarefas.Application.UsesCases.TarefasUseCases;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO;
using SWTarefas.Tests.TestsMoq.Common.AutoMapper;
using SWTarefas.Tests.TestsMoq.Common.Entities;
using SWTarefas.Tests.TestsMoq.Common.Repositories;

namespace SWTarefas.Tests.TestsMoq.UsesCases
{
    public class UpdateTarefasUseCaseTestMoq
    {
        [Fact]
        public async Task Success()
        {
            var listaTarefas = TarefasListBuilder.Build();
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente();

            var mapper = AutoMapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, tarefa);
            var tarefaWriteRepository = TarefaWriteRepositoryBuilder.Build(tarefa);

            var updateTarefasUseCase = new UpdateTarefasUseCase(tarefaWriteRepository, tarefaReadRepository, unitOfWork, mapper);

            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefa);

            var result = await updateTarefasUseCase.Execute(tarefaRequest);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(tarefa.Titulo);
            result.Descricao.Should().Be(tarefa.Descricao);
        }
    }
}
