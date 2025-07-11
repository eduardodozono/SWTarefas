﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SWTarefas.Domain.Entities;

namespace SWTarefas.Infrastructure.DataAcess.EF
{
    public class SWTarefasContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public SWTarefasContext(DbContextOptions<SWTarefasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
