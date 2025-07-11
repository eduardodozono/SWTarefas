﻿using Moq;
using SWTarefas.Domain.DTO.Tarefas;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess.EF.Interfaces.Tarefas;

namespace SWTarefas.Tests.TestsMoq.Common.Repositories.Tarefas
{
    public static class TarefaReadRepositoryBuilder
    {
        public static ITarefaReadRepository Build(IList<Tarefa>? listaTarefas, Tarefa? tarefa)
        {
            var tarefaReadRepository = new Mock<ITarefaReadRepository>();

            tarefaReadRepository.Setup(st => st.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(listaTarefas);

            tarefaReadRepository.Setup(st => st.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(tarefa);

            tarefaReadRepository.Setup(st => st.Filter(It.IsAny<FilterTarefa>(), It.IsAny<CancellationToken>())).ReturnsAsync(listaTarefas);

            return tarefaReadRepository.Object;
        }
    }
}