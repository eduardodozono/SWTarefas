using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using static SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Enums.StatusEnum;

namespace SWTarefas.Application.UsesCases.TarefasUseCasesSite.ViewModel
{
    public class TarefaViewModel
    {
        public int TarefaId { get; set; }

        [MaxLength(100, ErrorMessage = "O campo {0} tem o máximo de 400 caracteres.")]
        [Required(ErrorMessage ="O campo {0} é obrigatório.")]
        [DisplayName("Título")]
        public string Titulo { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(400, ErrorMessage = "O campo {0} tem o máximo de 400 caracteres.")]
        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("Data Prevista")]
        public DateOnly DataConclusaoPrevista { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data Realizada")]
        public DateOnly? DataConclusaoRealizada { get; set; } = null;

        public int Status { get; set; } = (int)TarefaStatus.Pendente;
    }
}
