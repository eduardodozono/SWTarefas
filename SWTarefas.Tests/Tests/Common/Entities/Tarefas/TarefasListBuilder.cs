using Bogus;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Tests.TestsMoq.Common.Entities.Tarefas
{
    public static class TarefasListBuilder
    {
        public static IList<Tarefa> Build()
        {
            var listaTarefas = new List<Tarefa>();

            var tarefa1 = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
                .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
                .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
                .RuleFor(t => t.DataConclusaoPrevista, new DateTime(2025, 1, 1))
                .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            var tarefa2 = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 2)
                 .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
                 .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
                 .RuleFor(t => t.DataConclusaoPrevista, new DateTime(2025, 1, 1))
                 .RuleFor(t => t.DataConclusaoRealizada, new DateTime(2025, 1, 1))
                 .RuleFor(t => t.Status, (int)TarefaStatus.Concluída);

            listaTarefas.Add(tarefa1);
            listaTarefas.Add(tarefa2);

            return listaTarefas;
        }

        public static List<Tarefa> Build_Create()
        {
            var listaTarefas = new List<Tarefa>();

            var tarefa1 = new Faker<Tarefa>()
                .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
                .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
                .RuleFor(t => t.DataConclusaoPrevista, new DateTime(2025, 1, 1))
                .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            var tarefa2 = new Faker<Tarefa>()
                 .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
                 .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
                 .RuleFor(t => t.DataConclusaoPrevista, new DateTime(2025, 1, 1))
                 .RuleFor(t => t.DataConclusaoRealizada, new DateTime(2025, 1, 1))
                 .RuleFor(t => t.Status, (int)TarefaStatus.Concluída);

            listaTarefas.Add(tarefa1);
            listaTarefas.Add(tarefa2);

            return listaTarefas;
        }

        public static List<Tarefa> Build_Titulo_Descricao(int paragraphTitulo = 1, int paragraphDescricao = 1)
        {
            var listaTarefas = new List<Tarefa>();

            var tarefa1 = new Faker<Tarefa>()
                .RuleFor(t => t.Titulo, (t) => t.Lorem.Paragraph(paragraphTitulo))
                .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(paragraphDescricao))
                .RuleFor(t => t.DataConclusaoPrevista, new DateTime(2025, 1, 1))
                .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            var tarefa2 = new Faker<Tarefa>()
                 .RuleFor(t => t.Titulo, (t) => t.Lorem.Paragraph(paragraphTitulo))
                 .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(paragraphDescricao))
                 .RuleFor(t => t.DataConclusaoPrevista, new DateTime(2025, 1, 1))
                 .RuleFor(t => t.DataConclusaoRealizada, new DateTime(2025, 1, 1))
                 .RuleFor(t => t.Status, (int)TarefaStatus.Concluída);

            listaTarefas.Add(tarefa1);
            listaTarefas.Add(tarefa2);

            return listaTarefas;
        }
    }
}
