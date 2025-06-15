using FluentAssertions;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write;
using SWTarefas.Domain.Entities;
using SWTarefas.Resources.Resources;
using SWTarefas.Tests.TestsMoq.Common.AutoMapper;
using SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas;
using SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas;

namespace SWTarefas.Tests.TestsMoq.UsesCases.Tarefas.Write
{
    public class UpdateTarefasUseCaseTestMoq
    {
        [Fact]
        public async Task Success_Tarefa_Pendente()
        {
            var listaTarefas = TarefasListBuilder.Build();
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefa);

            var result = await CreateUseCase(listaTarefas, tarefa).Execute(tarefaRequest);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(tarefa.Titulo);
            result.Descricao.Should().Be(tarefa.Descricao);
        }

        [Fact]
        public async Task Success_Tarefa_Concluida()
        {
            var listaTarefas = TarefasListBuilder.Build();
            var tarefa = TarefasBuilder.Build_Tarefa_Concluida();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefa);

            var result = await CreateUseCase(listaTarefas, tarefa).Execute(tarefaRequest);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(tarefa.Titulo);
            result.Descricao.Should().Be(tarefa.Descricao);
        }

        [Fact]
        public async Task Error_Tarefa_Nao_Existe()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(null, null).Execute(tarefaRequest);

            (await act.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.TarefaNaoExiste);
        }

        [Fact]
        public async Task Error_Tarefa_Sem_Id()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Sem_Id();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(null, tarefa).Execute(tarefaRequest);

            (await act.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.TarefaVazia);
        }

        [Fact]
        public async Task Error_Data_Prevista_Data_Realizada()
        {
            var listaTarefas = TarefasListBuilder.Build();
            var tarefa = TarefasBuilder.Build_Tarefa_Concluida_Data_Prevista_Superior_Data_Realizada();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(listaTarefas, tarefa).Execute(tarefaRequest);

            (await act.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.DataConclusaoInferiorDataPrevista);
        }

        [Fact]
        public async Task Error_Data_Realizada_Status_Pendente()
        {
            var listaTarefas = TarefasListBuilder.Build();
            var tarefa = TarefasBuilder.Build_Tarefa_Data_Realizada_Status_Pendente();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(listaTarefas, tarefa).Execute(tarefaRequest);

            (await act.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.StatusConcluidoErroApiDataRealizada);
        }

        [Fact]
        public async Task Error_Data_Realizada_Vazia_Status_Concluido()
        {
            var listaTarefas = TarefasListBuilder.Build();
            var tarefa = TarefasBuilder.Build_Tarefa_Data_Realizada_Vazia_Status_Concluido();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<UpdateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(listaTarefas, tarefa).Execute(tarefaRequest);

            (await act.Should().ThrowAsync<CustomBadRequestException>()).And._errors.Contains(SWTarefasMessagesExceptions.StatusPendenteErroApiDataRealizadaVazia);
        }

        private static UpdateTarefasUseCase CreateUseCase(IList<Tarefa>? listaTarefas = null, Tarefa? tarefa = null)
        {
            var mapper = AutoMapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, tarefa);
            var tarefaWriteRepository = TarefaWriteRepositoryBuilder.Build(tarefa);

            return new UpdateTarefasUseCase(tarefaWriteRepository, tarefaReadRepository, unitOfWork, mapper);
        }
    }
}
