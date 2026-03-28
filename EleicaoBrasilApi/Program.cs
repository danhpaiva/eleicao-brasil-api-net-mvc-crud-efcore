using EleicaoBrasilApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("EleicoesDb"));
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Habilita o Swagger independente do ambiente para os alunos testarem
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
