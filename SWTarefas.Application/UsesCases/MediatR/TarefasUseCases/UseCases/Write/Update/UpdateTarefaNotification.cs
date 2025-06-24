using MediatR;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Application.UsesCases.MediatR.TarefasUseCases.UseCases.Write.Update
{
    public class UpdateTarefaNotification : INotification
    {
        public Tarefa tarefa { get; set; }

        public UpdateTarefaNotification(Tarefa tarefa)
        {
            this.tarefa = tarefa;
        }
    }
}
