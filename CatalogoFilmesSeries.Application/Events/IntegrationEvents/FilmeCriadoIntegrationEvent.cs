namespace CatalogoFilmesSeries.Application.Events.IntegrationEvents;

public record FilmeCriadoIntegrationEvent(Filme FilmeCriado) : IIntegrationEvent;