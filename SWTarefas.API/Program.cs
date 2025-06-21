using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using SWTarefas.API.Filters;
using SWTarefas.CrossCutting.Extensions;
using SWTarefas.Infrastructure.DataAcess.EF;
using SWTarefas.SignalR.Hubs.Tarefas;
using SWTarefas.Workers.Workers.Tarefas;

const string AUTHENTICATION_TYPE = "Bearer";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(AUTHENTICATION_TYPE, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = AUTHENTICATION_TYPE
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = AUTHENTICATION_TYPE
                },
                Scheme = "oauth2",
                Name = AUTHENTICATION_TYPE,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddInfraExtension(builder.Configuration);
builder.Services.AddJWTExtension(builder.Configuration);
builder.Services.AddAutoMapperExtension();
builder.Services.AddUseCasesExtension();
builder.Services.AddMvc(options => options.Filters.Add(typeof(CustomFilterException)));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHealthChecks().AddDbContextCheck<SWTarefasContext>();
builder.Services.AddHostedService<TarefasDeleteVencidosWorker>();
builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<TarefaHub>("/tarefasSR");

app.MapHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy]= StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class ProgramApi { }