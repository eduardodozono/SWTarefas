using FluentAssertions;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Write;
using SWTarefas.Domain.Entities;
using SWTarefas.Tests.TestsMoq.Common.AutoMapper;
using SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas;
using SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas;

namespace SWTarefas.Tests.TestsMoq.UsesCases.Tarefas.Write
{
    public class CreateTarefaUseCaseTestMoq
    {
        [Fact]
        public async Task Sucess()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            var result = await CreateUseCase(tarefa).Execute(tarefaRequest);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(tarefa.Titulo);
            result.Descricao.Should().Be(tarefa.Descricao);
        }

        [Fact]
        public async Task Error_Titulo_Vazio()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Titulo_Vazio();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(tarefa).Execute(tarefaRequest);

            await act.Should().ThrowAsync<CustomBadRequestException>();
        }

        [Fact]
        public async Task Error_Titulo_Tamanho_Maximo()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Titulo_Tamanho_Max();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(tarefa).Execute(tarefaRequest);

            await act.Should().ThrowAsync<CustomBadRequestException>();
        }

        [Fact]
        public async Task Error_Descricao_Tamanho_Maximo()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Descricao_Tamanho_Max();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(tarefa).Execute(tarefaRequest);

            await act.Should().ThrowAsync<CustomBadRequestException>();
        }

        [Fact]
        public async Task Error_Data_Prevista_Vazia()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Data_Prevista_Vazia();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(tarefa).Execute(tarefaRequest);

            await act.Should().ThrowAsync<CustomBadRequestException>();
        }

        [Fact]
        public async Task Error_Status_Vazio()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Status_Vazio();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(tarefa).Execute(tarefaRequest);

            await act.Should().ThrowAsync<CustomBadRequestException>();
        }

        [Fact]
        public async Task Error_Status_Invalido()
        {
            var tarefa = TarefasBuilder.Build_Tarefa_Pendente_Status_Invalido();
            var mapper = AutoMapperBuilder.Build();
            var tarefaRequest = mapper.Map<CreateTarefaRequest>(tarefa);

            Func<Task> act = async () => await CreateUseCase(tarefa).Execute(tarefaRequest);

            await act.Should().ThrowAsync<CustomBadRequestException>();
        }

        private static CreateTarefaUseCase CreateUseCase(Tarefa tarefa)
        {
            var mapper = AutoMapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var tarefaWriteRepository = TarefaWriteRepositoryBuilder.Build(tarefa);

            return new CreateTarefaUseCase(tarefaWriteRepository, unitOfWork, mapper);

        }
    }
}
