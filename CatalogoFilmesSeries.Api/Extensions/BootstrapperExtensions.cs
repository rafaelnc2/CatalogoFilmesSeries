using CatalogoFilesSeries.Api.IoC;

namespace CatalogoFilesSeries.Api.Extensions;

public static class BootstrapperExtensions
{
    public static void AddBootstrapperRegistration(this IServiceCollection services, IConfiguration config)
    {
        new RootBootstrapper().BootstrapperRegisterServices(services, config);
    }
}