using Bogus;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;

namespace SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios
{
    public static class UsuariosLoginUseCaseRequestBuilder
    {
        public static UsuariosLoginUseCaseRequest Build()
        {
            var usuariosLoginUseCaseRequest = new Faker<UsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Email, (f) => f.Person.Email)
                .RuleFor(r => r.Password, (f) => f.Internet.Password(6));

            return usuariosLoginUseCaseRequest;
        }

        public static UsuariosLoginUseCaseRequest Build_Email_Vazio()
        {
            var usuariosLoginUseCaseRequest = new Faker<UsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Email, string.Empty)
                .RuleFor(r => r.Password, (f) => f.Internet.Password(6));

            return usuariosLoginUseCaseRequest;
        }

        public static UsuariosLoginUseCaseRequest Build_Email_Invalido()
        {
            var usuariosLoginUseCaseRequest = new Faker<UsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Email, (f) => f.Person.FirstName)
                .RuleFor(r => r.Password, (f) => f.Internet.Password(6));

            return usuariosLoginUseCaseRequest;
        }

        public static UsuariosLoginUseCaseRequest Build_Senha_Vazia()
        {
            var usuariosLoginUseCaseRequest = new Faker<UsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Email, (f) => f.Person.Email)
                .RuleFor(r => r.Password, string.Empty);

            return usuariosLoginUseCaseRequest;
        }

        public static UsuariosLoginUseCaseRequest Build_Senha_Invalida()
        {
            var usuariosLoginUseCaseRequest = new Faker<UsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Email, (f) => f.Person.Email)
                .RuleFor(r => r.Password, (f) => f.Internet.Password(5));

            return usuariosLoginUseCaseRequest;
        }

        public static UsuariosLoginUseCaseRequest Build_Custom(string email, string senha)
        {
            var usuariosLoginUseCaseRequest = new Faker<UsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Email, email)
                .RuleFor(r => r.Password, senha);

            return usuariosLoginUseCaseRequest;
        }
    }
}
