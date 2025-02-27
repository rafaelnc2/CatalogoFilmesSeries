using CatalogoFilmesSeries.Infrastructure.IoC;

namespace CatalogoFilesSeries.Api.Extensions;

public static class BootstrapperExtensions
{
    public static void AddBootstrapperRegistration(this IServiceCollection services)
    {
        new RootBootstrapper().BootstrapperRegisterServices(services);
    }
}