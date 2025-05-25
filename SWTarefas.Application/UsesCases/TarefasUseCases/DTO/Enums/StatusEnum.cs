using System.ComponentModel;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums
{
    public class StatusEnum
    {
        public enum TarefaStatus
        {
            [Description("Concluída")]
            Concluída = 1,
            [Description("Pendente")]
            Pendente = 2
        }
    }
}