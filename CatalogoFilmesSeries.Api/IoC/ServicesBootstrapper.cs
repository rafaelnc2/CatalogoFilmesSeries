using CatalogoFilmesSeries.Adapters.Outbound;
using CatalogoFilmesSeries.Application.Interfaces.Services;

namespace CatalogoFilesSeries.Api.IoC;

internal sealed class ServicesBootstrapper
{
    public void ServicesRegister(IServiceCollection services)
    {
        services.AddTransient<IShowInfoService, ShowInfoTMDBAdapter>();
    }
}