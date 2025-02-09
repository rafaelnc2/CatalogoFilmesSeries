namespace CatalogoFilmesSeries.Application.Interfaces.IIntegrationEvents;

public interface IIntegrationEventPublisher
{
    Task PublishAsync<T>(T integrationEvent) where T : IIntegrationEvent;
}