using CatalogoFilmesSeries.Adapters.Outbound.Repositories.Filmes;
using CatalogoFilmesSeries.Adapters.Outbound.Repositories.Series;
using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Filmes;
using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Series;

namespace CatalogoFilmesSeries.Infrastructure.IoC;

internal class RepositoriesBootstrapper
{
    public void RepositoriesServiceRegister(IServiceCollection services)
    {
        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();

        services.AddTransient<IShowInfoService, ShowInfoTMDBAdapter>();

        services.AddScoped<IFilmeReadRepository, FilmeReadRepository>();
        services.AddScoped<IFilmeWriteRepository, FilmeWriteRepository>();

        services.AddScoped<ISerieReadRepository, SerieReadRepository>();
        services.AddScoped<ISerieWriteRepository, SerieWriteRepository>();
    }
}