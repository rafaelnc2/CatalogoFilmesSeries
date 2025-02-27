using CatalogoFilesSeries.Api.Extensions;
using CatalogoFilmesSeries.Adapters.Outbound;
using CatalogoFilmesSeries.Adapters.Outbound.IntegrationEventPublishers;
using CatalogoFilmesSeries.Adapters.Outbound.Repositories.Filmes;
using CatalogoFilmesSeries.Adapters.Outbound.Repositories.Series;
using CatalogoFilmesSeries.Application.Interfaces.IIntegrationEvents;
using CatalogoFilmesSeries.Application.Interfaces.Services;
using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Filmes;
using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Series;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();

builder.Services.AddTransient<IShowInfoService, ShowInfoTMDBAdapter>();

builder.Services.AddScoped<IFilmeReadRepository, FilmeReadRepository>();
builder.Services.AddScoped<IFilmeWriteRepository, FilmeWriteRepository>();

builder.Services.AddScoped<ISerieReadRepository, SerieReadRepository>();
builder.Services.AddScoped<ISerieWriteRepository, SerieWriteRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediator();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();