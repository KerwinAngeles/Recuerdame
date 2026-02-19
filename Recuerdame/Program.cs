using Microsoft.EntityFrameworkCore;
using Recuerdame.Filters;
using Recuerdame.Interfaces;
using Recuerdame.Persistence;
using Recuerdame.Repositories;
using Recuerdame.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Controladores con filtro global de excepciones ──────────────────────────
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
