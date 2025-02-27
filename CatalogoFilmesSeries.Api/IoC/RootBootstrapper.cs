using CatalogoFilmesSeries.Adapters.Outbound.IntegrationEventPublishers;
using CatalogoFilmesSeries.Application.Interfaces.IIntegrationEvents;
using CatalogoFilmesSeries.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilesSeries.Api.IoC;

public sealed class RootBootstrapper
{
    public void BootstrapperRegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, opt) => opt
            .UseSqlServer(config.GetConnectionString("SqlServer")));
            
        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();
        
        new ServicesBootstrapper().ServicesRegister(services);
        
        new RepositoriesBootstrapper().RepositoriesServiceRegister(services);
    }
}