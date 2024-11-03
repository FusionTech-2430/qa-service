using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;
using qa_service.Repositories.Interfaces;
using qa_service.UseCases.Interfaces;
using qa_service.UseCases.Implementations;
using questions_service.Repositories.Implementations;
using System;

var builder = WebApplication.CreateBuilder(args);

// Cargar las variables de entorno desde el archivo .env
Env.Load();

// Construir la cadena de conexión desde las variables de entorno
var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? throw new InvalidOperationException("DB_SERVER is not set in environment variables.");
var database = Environment.GetEnvironmentVariable("DB_NAME") ?? throw new InvalidOperationException("DB_NAME is not set in environment variables.");
var user = Environment.GetEnvironmentVariable("DB_USER") ?? throw new InvalidOperationException("DB_USER is not set in environment variables.");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD is not set in environment variables.");
var trustServerCertificate = Environment.GetEnvironmentVariable("DB_TRUST_SERVER_CERTIFICATE") ?? throw new InvalidOperationException("DB_TRUST_SERVER_CERTIFICATE is not set in environment variables.");

var connectionString = $"Server={server};Database={database};User Id={user};Password={password};TrustServerCertificate={trustServerCertificate}";

// Validar conexión a la base de datos
try
{
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
    }
    Console.WriteLine("Database connection established successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to establish database connection: {ex.Message}");
    throw new InvalidOperationException("Could not establish a connection to the database.");
}

// Registrar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyectar el connectionString en el repositorio
builder.Services.AddScoped<IQuestionRepository>(_ => new QuestionRepository(connectionString));
builder.Services.AddScoped<ICreateQuestionCU, CreateQuestionCU>();
builder.Services.AddScoped<IUpdateQuestionCU, UpdateQuestionCU>();
builder.Services.AddScoped<IDeleteQuestionCU, DeleteQuestionCU>();
builder.Services.AddScoped<IGetQuestionCU, GetQuestionCU>();

var app = builder.Build();

// Configurar Swagger en el pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
