using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjetoSW.Infrastructure.DataAcess.Repository.UnitOfWork;
using SWTarefas.Application.Cryptography;
using SWTarefas.Infrastructure.DataAcess;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Tarefas;
using SWTarefas.Infrastructure.DataAcess.Interfaces.UnitOfWork;
using SWTarefas.Infrastructure.DataAcess.Interfaces.Usuarios;
using SWTarefas.Infrastructure.DataAcess.Repository.Tarefas;
using SWTarefas.Infrastructure.DataAcess.Repository.Usuarios;
using SWTarefas.Infrastructure.Security.Tokens.Acess;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;

namespace SWTarefas.CrossCutting.Extensions
{
    public static class AddInfrastructureExtension
    {
        public static IServiceCollection AddInfraExtension(this IServiceCollection services, IConfiguration configuration)
        {
            if (!configuration.IsUnitTestEnviroment())
            {
                // Para utlizar o banco de dados SQL Server descomentar a linha abaixo e comentar a linha do banco em memoria
                services.AddDbContext<SWTarefasContext>(sql => sql.UseSqlServer(configuration.GetConnectionString("Connection")));

                // Banco de dados em memoria foi colocado somente para facilitar os testes mas o real seria o Sql Server
                //services.AddDbContext<SWTarefasContext>(sql => sql.UseInMemoryDatabase(configuration.GetConnectionString("InMemory")!));
            }

            services.AddScoped<ICustomEncripter, CustomEncripter>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITarefaWriteRepository, TarefaRepository>();
            services.AddScoped<ITarefaReadRepository, TarefaRepository>();
            services.AddScoped<ITarefaDeleteRepository, TarefaRepository>();

            services.AddScoped<IUsuarioReadRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioWriteRepository, UsuarioRepository>();

            return services;
        }

        public static IServiceCollection AddJWTExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = uint.Parse(configuration.GetSection("Settings:Jwt:ExpirationTimeMinutes").Value ?? "10");
            var signingKey = configuration.GetSection("Settings:Jwt:SigningKey").Value ?? string.Empty;
            var key = Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IJwtTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey));
            services.AddScoped<IAcessTokenValidator>(option => new AcessTokenValidator(signingKey));

            return services;
        }

        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            if (configuration.GetSection("InMemoryTest").Value == null)
                return false;

            return Boolean.Parse(configuration.GetSection("InMemoryTest").Value!);
        }

    }
}
