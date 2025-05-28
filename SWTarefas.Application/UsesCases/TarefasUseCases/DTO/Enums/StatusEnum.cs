using System.ComponentModel;

namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums
{
    public enum TarefaStatus
    {
        [Description("Concluída")]
        Concluída = 1,
        [Description("Pendente")]
        Pendente = 2
    }
}