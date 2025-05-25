using SWTarefas.API.Filters;
using SWTarefas.CrossCutting.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddInfraExtension(builder.Configuration);
builder.Services.AddAutoMapperExtension();
builder.Services.AddUseCasesExtension();
builder.Services.AddMvc(options => options.Filters.Add(typeof(CustomFilterException)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
