using MediatR;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Write
{
    public class CreateTarefaNotification : INotification
    {
        public Tarefa tarefa { get; set; }

        public CreateTarefaNotification(Tarefa tarefa)
        {
            this.tarefa = tarefa;
        }
    }
}
