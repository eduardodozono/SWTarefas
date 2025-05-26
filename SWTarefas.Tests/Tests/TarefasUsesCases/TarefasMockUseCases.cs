using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SWTarefas.Application.UsesCases.TarefasUseCases.DTO.Profiles;
using SWTarefas.Domain.Entities;
using SWTarefas.Infrastructure.DataAcess;

namespace SWTarefas.Tests.Tests.TarefasUsesCases
{
    public class TarefasMockUseCases
    {
        public IMapper GetIMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            return mapper;
        }

        public SWTarefasContext GetDatabase(bool criar)
        {
            var inMemoryOptions = new DbContextOptionsBuilder<SWTarefasContext>()
                .UseInMemoryDatabase("ef05bd7d-0d35-442b-8f84-235aece7cbad").Options;

            var tarefasContext = new SWTarefasContext(inMemoryOptions);

            if (criar)
            {
                using (var context = new SWTarefasContext(inMemoryOptions))
                {
                    context.Tarefas.Add(new Tarefa
                    {
                        Titulo = "Titulo",
                        Descricao = "Descricao",
                        DataConclusaoPrevista = new DateOnly(2025, 1, 1),
                        DataConclusaoRealizada = new DateOnly(2025, 1, 1),
                        Status = 2
                    });

                    context.Tarefas.AddAsync(new Tarefa
                    {
                        Titulo = "Titulo 2",
                        Descricao = "Descricao 2",
                        DataConclusaoPrevista = new DateOnly(2025, 1, 1),
                        DataConclusaoRealizada = new DateOnly(2025, 1, 1),
                        Status = 2
                    });

                    context.SaveChangesAsync();
                }
            }

            return new SWTarefasContext(inMemoryOptions);
        }
    }
}
