using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SWTarefas.API.Filters;
using SWTarefas.CrossCutting.Extensions;
using SWTarefas.Infrastructure.DataAcess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddInfraExtension(builder.Configuration);
builder.Services.AddAutoMapperExtension();
builder.Services.AddUseCasesExtension();
builder.Services.AddMvc(options => options.Filters.Add(typeof(CustomFilterException)));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHealthChecks().AddDbContextCheck<SWTarefasContext>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class ProgramApi { }