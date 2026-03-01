using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Recuerdame.Filters;
using Recuerdame.Interfaces;
using Recuerdame.Persistence;
using Recuerdame.Repositories;
using Recuerdame.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:8080");

// ── Controladores con filtro global de excepciones ──────────────────────────
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── Base de datos ────────────────────────────────────────────────────────────
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Repositorios ─────────────────────────────────────────────────────────────
builder.Services.AddScoped<IRepositorioCategoriaMedicamento, RepositorioCategoriaMedicamento>();
builder.Services.AddScoped<IRepositorioMedicamento, RepositorioMedicamento>();
builder.Services.AddScoped<IRepositorioTomaProgramada, RepositorioTomaProgramada>();

// ── Servicios ────────────────────────────────────────────────────────────────
builder.Services.AddScoped<ServicioMedicamento>();
builder.Services.AddScoped<ServicioCategoriaMedicamento>();
builder.Services.AddScoped<ServicioTomaProgramada>();

// ── CORS ─────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ── OpenAPI ──────────────────────────────────────────────────────────────────
builder.Services.AddOpenApi();

var app = builder.Build();


app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
                       Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var retries = 5;
    while (retries > 0)
    {
        try
        {
            db.Database.Migrate();
            Console.WriteLine("✅ Migraciones aplicadas correctamente.");
            break;
        }
        catch (Exception ex)
        {
            retries--;
            Console.WriteLine($"⏳ Esperando SQL... Reintentos restantes: {retries}");
            Thread.Sleep(5000);
        }
    }
}

app.UseRouting();
app.UseCors("Frontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
