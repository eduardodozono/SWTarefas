using Bogus;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Tests.TestsMoq.Common.Entities
{
    public static class TarefasBuilder
    {
        public static Tarefa Build_Tarefa_Pendente()
        {
            var tarefaPendente = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Concluida()
        {
            var tarefaConcluida = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 2)
                 .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
                 .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
                 .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1))
                 .RuleFor(t => t.DataConclusaoRealizada, new DateOnly(2025, 1, 1))
                 .RuleFor(t => t.Status, (int)TarefaStatus.Concluída);

            return tarefaConcluida;
        }

        public static Tarefa Build_Tarefa_Pendente_Titulo_Vazio()
        {
            var tarefaPendente = new Faker<Tarefa>()
               .RuleFor(t => t.Titulo, string.Empty).RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Pendente_Titulo_Tamanho_Max()
        {
            var tarefaPendente = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Titulo, (t) => t.Lorem.Paragraph(5))
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Pendente_Descricao_Tamanho_Max()
        {
            var tarefaPendente = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(12))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Pendente_Data_Prevista_Vazia()
        {
            var tarefaPendente = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Pendente_Status_Vazio()
        {
            var tarefaPendente = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1));

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Pendente_Status_Invalido()
        {
            var tarefaPendente = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.Status, 3);

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Pendente_Sem_Id()
        {
            var tarefaPendente = new Faker<Tarefa>()
               .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Concluida_Data_Prevista_Superior_Data_Realizada()
        {
            var tarefaPendente = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 2))
               .RuleFor(t => t.DataConclusaoRealizada, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.Status, (int)TarefaStatus.Concluída);

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Data_Realizada_Status_Pendente()
        {
            var tarefaPendente = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.DataConclusaoRealizada, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.Status, (int)TarefaStatus.Pendente);

            return tarefaPendente;
        }

        public static Tarefa Build_Tarefa_Data_Realizada_Vazia_Status_Concluido()
        {
            var tarefaPendente = new Faker<Tarefa>().RuleFor(t => t.TarefaId, 1)
               .RuleFor(t => t.Titulo, (t) => t.Person.FullName)
               .RuleFor(t => t.Descricao, (t) => t.Lorem.Paragraph(1))
               .RuleFor(t => t.DataConclusaoPrevista, new DateOnly(2025, 1, 1))
               .RuleFor(t => t.Status, (int)TarefaStatus.Concluída);

            return tarefaPendente;
        }
    }
}
