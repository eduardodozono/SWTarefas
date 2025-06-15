using Bogus;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Request;

namespace SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas
{
    public static class FilterTarefaRequestBuilder
    {
        public static FilterTarefaRequest Build(string titulo = "", string? descricao = "", TarefaStatus status = TarefaStatus.Pendente)
        {
            var filterTarefaRequest = new Faker<FilterTarefaRequest>()
                .RuleFor(f => f.Titulo, titulo)
                .RuleFor(f => f.Status, (int)status)
                .RuleFor(f => f.Descricao, descricao);

            return filterTarefaRequest;
        }
    }
}
