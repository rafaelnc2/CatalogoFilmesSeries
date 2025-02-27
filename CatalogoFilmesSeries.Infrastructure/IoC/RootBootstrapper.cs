namespace CatalogoFilmesSeries.Infrastructure.IoC;

public sealed class RootBootstrapper
{
    public void BootstrapperRegisterServices(IServiceCollection services)
    {
        new RepositoriesBootstrapper().RepositoriesServiceRegister(services);
    }
}