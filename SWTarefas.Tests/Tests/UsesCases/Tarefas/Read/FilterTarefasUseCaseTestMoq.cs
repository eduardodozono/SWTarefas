using FluentAssertions;
using SWTarefas.Application.Exceptions;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;
using SWTarefas.Application.UsesCases.TarefasUseCases.UseCases.Read;
using SWTarefas.Resources.Resources;
using SWTarefas.Tests.TestsMoq.Common.AutoMapper;
using SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas;
using SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas;

namespace SWTarefas.Tests.TestsMoq.UsesCases.Tarefas.Read
{
    public class FilterTarefasUseCaseTestMoq
    {
        [Fact]
        public async Task Success()
        {
            var listaTarefas = TarefasListBuilder.Build();

            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, listaTarefas[0]);

            var mapper = AutoMapperBuilder.Build();

            var filterTarefas = new FilterTarefasUseCase(tarefaReadRepository, mapper);

            var filterTarefaRequest = FilterTarefaRequestBuilder.Build(listaTarefas[0].Titulo, listaTarefas[0].Descricao, (TarefaStatus)listaTarefas[0].Status);

            var result = await filterTarefas.Execute(filterTarefaRequest);

            result.Should().NotBeNull();
            result.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Error_Status_Invalido()
        {
            var listaTarefas = TarefasListBuilder.Build();

            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, listaTarefas[0]);

            var mapper = AutoMapperBuilder.Build();

            var filterTarefas = new FilterTarefasUseCase(tarefaReadRepository, mapper);

            var filterTarefaRequest = FilterTarefaRequestBuilder.Build(listaTarefas[0].Titulo, listaTarefas[0].Descricao, (TarefaStatus)3);

            Func<Task> act = async () => await filterTarefas.Execute(filterTarefaRequest);

            var resultErros = await act.Should().ThrowAsync<CustomBadRequestException>();
            resultErros.And._errors.Should().Contain(SWTarefasMessagesExceptions.StatusInvalido);
        }

        [Fact]
        public async Task Error_Titulo_Max_Cacteres()
        {
            var listaTarefas = TarefasListBuilder.Build_Titulo_Descricao(5, 1);

            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, listaTarefas[0]);

            var mapper = AutoMapperBuilder.Build();

            var filterTarefas = new FilterTarefasUseCase(tarefaReadRepository, mapper);

            var filterTarefaRequest = FilterTarefaRequestBuilder.Build(listaTarefas[0].Titulo, listaTarefas[0].Descricao, (TarefaStatus)listaTarefas[0].Status);

            Func<Task> act = async () => await filterTarefas.Execute(filterTarefaRequest);

            var resultErros = await act.Should().ThrowAsync<CustomBadRequestException>();
            resultErros.And._errors.Should().Contain(SWTarefasMessagesExceptions.TItuloMaximoCaracteres);
        }

        [Fact]
        public async Task Error_Descricao_Max_Cacteres()
        {
            var listaTarefas = TarefasListBuilder.Build_Titulo_Descricao(1, 20);

            var tarefaReadRepository = TarefaReadRepositoryBuilder.Build(listaTarefas, listaTarefas[0]);

            var mapper = AutoMapperBuilder.Build();

            var filterTarefas = new FilterTarefasUseCase(tarefaReadRepository, mapper);

            var filterTarefaRequest = FilterTarefaRequestBuilder.Build(listaTarefas[0].Titulo, listaTarefas[0].Descricao, (TarefaStatus)listaTarefas[0].Status);

            Func<Task> act = async () => await filterTarefas.Execute(filterTarefaRequest);

            var resultErros = await act.Should().ThrowAsync<CustomBadRequestException>();
            resultErros.And._errors.Should().Contain(SWTarefasMessagesExceptions.DescricaoMaximoCaracteres);
        }
    }
}
