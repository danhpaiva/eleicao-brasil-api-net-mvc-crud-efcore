using EleicaoBrasilApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("EleicoesDb"));
builder.Services.AddControllers();

// 1. Adicione estas duas linhas abaixo:
builder.Services.AddEndpointsApiExplorer(); // Necessário para encontrar os endpoints
builder.Services.AddSwaggerGen();            // REGISTRA O SERVIÇO QUE ESTÁ FALTANDO

builder.Services.AddOpenApi(); // Isso é para o Scalar/OpenApi (opcional se usar Swagger)

var app = builder.Build();

// Habilita o Swagger independente do ambiente para vcs testarem
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
//Daniel Paiva - Exemplo de API RESTful para Eleições no Brasil usando .NET 10, Entity Framework Core e In-Memory Database.
