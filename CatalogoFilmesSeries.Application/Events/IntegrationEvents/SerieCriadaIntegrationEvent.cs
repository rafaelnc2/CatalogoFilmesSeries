namespace CatalogoFilmesSeries.Application.Events.IntegrationEvents;

public record SerieCriadaIntegrationEvent(Serie Serie) : IIntegrationEvent;