﻿using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas
{
    public interface ITarefaWriteRepository
    {
        public Task<Tarefa> Create(Tarefa tarefa, CancellationToken token = default);
        public Tarefa Update(Tarefa tarefa);
    }
}
