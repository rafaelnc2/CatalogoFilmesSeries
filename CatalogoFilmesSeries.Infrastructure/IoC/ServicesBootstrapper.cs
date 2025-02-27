namespace CatalogoFilmesSeries.Infrastructure.IoC;

internal sealed class ServicesBootstrapper
{
    public void ServicesRegister(IServiceCollection services)
    {
        services.AddTransient<IShowInfoService, ShowInfoTMDBAdapter>();
    }
}