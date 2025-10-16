using Microsoft.EntityFrameworkCore;
using PropostaHub.Core.Ports;
using PropostaHub.Core.UseCases;
using PropostaHub.Infrastructure.Adapters.Persistence;
using PropostaHub.Infrastructure.Adapters.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuracao do DbContext com SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("PropostaHub.Infrastructure")));

// Dependency Injection - Ports & Adapters
builder.Services.AddScoped<IPropostaRepository, PropostaRepository>();
builder.Services.AddScoped<IPropostaService, PropostaAppService>();

// Use Cases
builder.Services.AddScoped<CriarPropostaUseCase>();
builder.Services.AddScoped<ListarPropostasUseCase>();
builder.Services.AddScoped<AlterarStatusPropostaUseCase>();

// Logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Aplica migrations automaticamente em desenvolvimento
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
