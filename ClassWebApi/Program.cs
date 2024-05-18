using ClassWebApi.Application;
using ClassWebApi.Domain.Interfaces;
using ClassWebApi.Infrastructure;
using ClassWebApi.Infrastructure.Interfaces;
using ClassWebApi.Infrastructure.Repositories;
using Dapper;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IDbConnection>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("SqlConnection");
    if (connectionString == null)
    {
        throw new InvalidOperationException("Connection string 'SqlConnection' not found in appsettings.json");
    }
    return new SqlConnection(connectionString);
});
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IAlunoTurmaService, AlunoTurmaService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
builder.Services.AddScoped<IAlunoTurmaRepository, AlunoTurmaRepository>();
builder.Services.AddSingleton<DatabaseInitializer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
        initializer.InitializeDatabase();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
