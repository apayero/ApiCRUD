using ApiCRUD;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Inicio de área de los servicios: Alex Mota y Payero pueden actualizar código juntos
// Otra prueba de Alex - Noviembre 7 3:03 p.m.

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
opciones.UseSqlServer("name=DefaultConnection"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var OrigenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(politica =>
    {
        politica.WithOrigins(OrigenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
