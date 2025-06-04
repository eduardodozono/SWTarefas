using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.DataMappings
{
    public class UsuarioDataMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(t => t.UsuarioId);
            builder.Property(t=> t.Nome).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Email).HasMaxLength(300).IsRequired();
            builder.Property(t => t.Senha).HasMaxLength(400).IsRequired();

            builder.ToTable("Usuario");
        }
    }
}
