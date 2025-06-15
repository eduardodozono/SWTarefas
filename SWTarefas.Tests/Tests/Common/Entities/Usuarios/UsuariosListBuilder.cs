using System.Collections.Generic;
using Bogus;
using SWTarefas.Domain.Entities;
using SWTarefas.Tests.TestsMoq.Common.Cryptography;

namespace SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios
{
    public static class UsuariosListBuilder
    {
        public static List<Usuario> Build()
        {
            var listaUsuarios = new List<Usuario>();

            var Usuario1 = new Faker<Usuario>()
                               .RuleFor(r => r.UsuarioId, 1)
                               .RuleFor(r => r.UsuarioIdentifier, Guid.NewGuid())
                               .RuleFor(r => r.Nome, (f) => f.Person.FirstName)
                               .RuleFor(r => r.Email, (f) => f.Person.Email)
                               .RuleFor(r => r.Senha, (f) => f.Internet.Password(6));

            var Usuario2 = new Faker<Usuario>()
               .RuleFor(r => r.UsuarioId, 2)
               .RuleFor(r => r.UsuarioIdentifier, Guid.NewGuid())
               .RuleFor(r => r.Nome, (f) => f.Person.FirstName)
               .RuleFor(r => r.Email, (f) => f.Person.Email)
               .RuleFor(r => r.Senha, (f) => f.Internet.Password(6));


            listaUsuarios.Add(Usuario1);
            listaUsuarios.Add(Usuario2);

            return listaUsuarios;
        }

        public static List<Usuario> Build_Encrypt(string senha)
        {
            var listaUsuarios = new List<Usuario>();

            var customEncripter = CustomEncripterBuilder.Build();

            var Usuario1 = new Faker<Usuario>()
                   .RuleFor(r => r.UsuarioId, 1)
                   .RuleFor(r => r.UsuarioIdentifier, Guid.NewGuid())
                   .RuleFor(r => r.Nome, (f) => f.Person.FirstName)
                   .RuleFor(r => r.Email, (f) => f.Person.Email)
                   .RuleFor(r => r.Senha, customEncripter.Encrypt(senha));

            var Usuario2 = new Faker<Usuario>()
               .RuleFor(r => r.UsuarioId, 2)
               .RuleFor(r => r.UsuarioIdentifier, Guid.NewGuid())
               .RuleFor(r => r.Nome, (f) => f.Person.FirstName)
               .RuleFor(r => r.Email, (f) => f.Person.Email)
               .RuleFor(r => r.Senha, (f) => customEncripter.Encrypt(senha));

            listaUsuarios.Add(Usuario1);
            listaUsuarios.Add(Usuario2);

            return listaUsuarios;
        }
    }
}
