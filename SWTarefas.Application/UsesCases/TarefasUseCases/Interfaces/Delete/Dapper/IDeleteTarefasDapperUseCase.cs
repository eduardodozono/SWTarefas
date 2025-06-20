﻿namespace SWTarefas.Application.UsesCases.TarefasUseCases.Interfaces.Delete.Dapper
{
    public interface IDeleteTarefasDapperUseCase
    {
        public Task<int> Execute(int tarefaId, CancellationToken token = default);
    }
}
