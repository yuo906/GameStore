using System.Reflection;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 連接db
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services
        .AddSqlite<GameStoreContext>(connString)
        .AddScoped<GameService>()
        .AddEndpointsApiExplorer()
        .AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "GameStore API",
                Description = "An ASP.NET Core Web API for managing Game Store items",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();
