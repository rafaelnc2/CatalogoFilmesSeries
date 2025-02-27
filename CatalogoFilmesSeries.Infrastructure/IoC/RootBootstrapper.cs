namespace CatalogoFilmesSeries.Infrastructure.IoC;

public sealed class RootBootstrapper
{
    public void BootstrapperRegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();
        
        new ServicesBootstrapper().ServicesRegister(services);
        
        new RepositoriesBootstrapper().RepositoriesServiceRegister(services);
    }
}