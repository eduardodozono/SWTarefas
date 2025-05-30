﻿namespace SWTarefas.Application.UsesCases.TarefasUseCases.DTO
{
    public class GetByIdTarefaResponse : TarefaBaseUseCase
    {
        public int TarefaId { get; init; }

        public GetByIdTarefaResponse(int tarefaId, string titulo, string? descricao, DateOnly? dataConclusaoPrevista, DateOnly? dataConclusaoRealizada, int status)
        {
            TarefaId = tarefaId;
            Titulo = titulo;
            Descricao = descricao;
            DataConclusaoPrevista = dataConclusaoPrevista;
            DataConclusaoRealizada = dataConclusaoRealizada;
            Status = status;
        }
    }
}
