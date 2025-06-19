using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.EF.DataMappings
{
    internal class TarefaDataMapping : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(t => t.TarefaId);

            builder.Property(t => t.Titulo).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Descricao).HasMaxLength(400);
            builder.Property(t => t.Status).IsRequired();

            builder.ToTable("Tarefa");
        }
    }
}
