using mega_webAPI.Context;
using mega_webAPI.Data.interfaces;
using mega_webAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//CORS configuration to connect with angular

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
//create variable for conexion
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                        builder.Configuration.GetConnectionString("Connection");

//register service for conexion
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString)
);



//register repositories
builder.Services.AddScoped<IMovie, MovieRepository>();
builder.Services.AddScoped<ITvShow, TvShowRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(8080); // Esto configura Kestrel para escuchar en todas las interfaces de red en el puerto 80
});
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); // Ruta del endpoint Swagger JSON
        c.RoutePrefix = string.Empty; // Swagger UI en la raíz
    });
//}

//app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
