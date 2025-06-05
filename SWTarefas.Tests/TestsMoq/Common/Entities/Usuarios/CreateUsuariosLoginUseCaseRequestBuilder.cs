using Bogus;
using SWTarefas.Application.UsesCases.UsuariosUseCases.DTO;

namespace SWTarefas.Tests.TestsMoq.Common.Entities.Usuarios
{
    public static class CreateUsuariosLoginUseCaseRequestBuilder
    {
        public static CreateUsuariosLoginUseCaseRequest Build()
        {
            var createUsuariosLoginUseCaseRequest = new Faker<CreateUsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Nome, (f) => f.Person.FirstName)
                .RuleFor(r => r.Email, (f) => f.Person.Email)
                .RuleFor(r => r.Password, (f) => f.Internet.Password(6));

            return createUsuariosLoginUseCaseRequest;
        }

        public static CreateUsuariosLoginUseCaseRequest Build_Email_Vazio()
        {
            var createUsuariosLoginUseCaseRequest = new Faker<CreateUsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Nome, (f) => f.Person.FirstName)
                .RuleFor(r => r.Email, (f) => string.Empty)
                .RuleFor(r => r.Password, (f) => f.Internet.Password(6));

            return createUsuariosLoginUseCaseRequest;
        }

        public static CreateUsuariosLoginUseCaseRequest Build_Email_Invalido()
        {
            var createUsuariosLoginUseCaseRequest = new Faker<CreateUsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Nome, (f) => f.Person.FirstName)
                .RuleFor(r => r.Email, (f) => f.Person.FirstName)
                .RuleFor(r => r.Password, (f) => f.Internet.Password(6));

            return createUsuariosLoginUseCaseRequest;
        }

        public static CreateUsuariosLoginUseCaseRequest Build_Senha_Vazia()
        {
            var createUsuariosLoginUseCaseRequest = new Faker<CreateUsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Nome, (f) => f.Person.FirstName)
                .RuleFor(r => r.Email, (f) => f.Person.Email)
                .RuleFor(r => r.Password, string.Empty);

            return createUsuariosLoginUseCaseRequest;
        }

        public static CreateUsuariosLoginUseCaseRequest Build_Senha_Invalida()
        {
            var createUsuariosLoginUseCaseRequest = new Faker<CreateUsuariosLoginUseCaseRequest>()
                .RuleFor(r => r.Nome, (f) => f.Person.FirstName)
                .RuleFor(r => r.Email, (f) => f.Person.Email)
                .RuleFor(r => r.Password, (f) => f.Internet.Password(5));

            return createUsuariosLoginUseCaseRequest;
        }
    }
}
