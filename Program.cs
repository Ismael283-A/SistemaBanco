using Microsoft.AspNetCore.Http.Connections;
using Servidor.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ConnectionOptions>(builder.Configuration.GetSection("ConnectionOptions"));
// Registrar servicio
builder.Services.AddSingleton<IBancoService, BancoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Permite servir index.html y demás archivos estáticos
app.UseDefaultFiles();  // sirve wwwroot/index.html
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class ConnectionOptions
{
    public string Cadena { get; set; }
}