using CatalogoFilmesSeries.Adapters.Outbound.Repositories.Filmes;
using CatalogoFilmesSeries.Adapters.Outbound.Repositories.Series;
using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Filmes;
using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Series;

namespace CatalogoFilmesSeries.Infrastructure.IoC;

internal sealed class RepositoriesBootstrapper
{
    public void RepositoriesServiceRegister(IServiceCollection services)
    {
        services.AddScoped<IFilmeReadRepository, FilmeReadRepository>();
        services.AddScoped<IFilmeWriteRepository, FilmeWriteRepository>();

        services.AddScoped<ISerieReadRepository, SerieReadRepository>();
        services.AddScoped<ISerieWriteRepository, SerieWriteRepository>();
    }
}