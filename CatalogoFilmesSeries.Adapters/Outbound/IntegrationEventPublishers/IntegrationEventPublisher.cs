using CatalogoFilmesSeries.Application.Interfaces.IIntegrationEvents;

namespace CatalogoFilmesSeries.Adapters.Outbound.IntegrationEventPublishers;

public sealed class IntegrationEventPublisher : IIntegrationEventPublisher
{
    public Task PublishAsync<T>(T integrationEvent) where T : IIntegrationEvent
    {
        throw new NotImplementedException();
    }
}