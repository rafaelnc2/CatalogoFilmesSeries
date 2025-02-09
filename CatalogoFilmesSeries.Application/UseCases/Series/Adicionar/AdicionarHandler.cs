using CatalogoFilmesSeries.Application.Interfaces.Repositories.Series;
using CatalogoFilmesSeries.Domain.ValueObjects;

namespace CatalogoFilmesSeries.Application.UseCases.Series.Adicionar;

public sealed class AdicionarHandler : IRequestHandler<AdicionarCommand, ApiResult<AdicionarResponse>>
{
    private readonly ILogger<AdicionarHandler> _logger;
    
    private readonly IExternalShowInfoService _externalShowInfoService;
    private readonly ISerieWriteRepository _serieWriteRepository;
    private readonly ISerieReadRepository _serieReadRepository;
    private readonly IIntegrationEventPublisher _integrationEventPublisher;
 
    public AdicionarHandler(ILogger<AdicionarHandler> logger, IExternalShowInfoService externalShowInfoService, 
        ISerieWriteRepository serieWriteRepository, ISerieReadRepository serieReadRepository, IIntegrationEventPublisher integrationEventPublisher)
    {
        _logger = logger;
        
        _externalShowInfoService = externalShowInfoService;
        _serieWriteRepository = serieWriteRepository;
        _serieReadRepository = serieReadRepository;

        _integrationEventPublisher = integrationEventPublisher;
    }
    
    public async Task<ApiResult<AdicionarResponse>> Handle(AdicionarCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hanlder para criação de um novo Filme!");
        
        var movieExists = await _serieReadRepository.SearchByNameAsync(command.Titulo);

        if (movieExists is not null)
        {
            _logger.LogWarning("Titulo informado já existe");
            return ApiResult<AdicionarResponse>.BadRequest($"Titulo informado já existe com ID {movieExists.Id}");
        }
        
        ImdbInfoVo imdbInfo = await GetImdbInfoAsync(command.Titulo);

        var serie = Serie.Create(
            command.Titulo, 
            command.TituloOriginal, 
            command.AnoLancamento, 
            command.ClassificacaoEtaria, 
            command.Sinopse, 
            command.UrlImagem, 
            command.Termporadas, 
            command.QuantidadeEpisodios, 
            command.DuracaoEpisodios, imdbInfo
        );
        
        if (serie.HasErrors)
        {
            _logger.LogWarning("Dados inválidos para a inclusão de uma Série");
            return ApiResult<AdicionarResponse>.BadRequest("Dados inválidos para a inclusão de uma Série", serie.Errors);
        }
        
        await _serieWriteRepository.AddAsync(serie);
        
        _logger.LogInformation("Série criada com sucesso!");

        await PublishEventsAsync(serie);

        var response = (AdicionarResponse)serie;
        
        return ApiResult<AdicionarResponse>.Created(response);
    }
    
    private async Task<ImdbInfoVo> GetImdbInfoAsync(string titulo)
    {
        var imdbInfoDto = await _externalShowInfoService.GetImdbDataAsync(titulo);

        return new(imdbInfoDto?.Popularity, imdbInfoDto?.VoteAverage, imdbInfoDto?.VoteCount);
    } 
    
    private async Task PublishEventsAsync(Serie serie)
    {
        _logger.LogInformation("Criando evento!");
        
        var serieCriadaIntegrationEvent = new SerieCriadaIntegrationEvent(serie);
        
        await _integrationEventPublisher.PublishAsync(serieCriadaIntegrationEvent);
        
        _logger.LogInformation("Evento publicado com sucesso!");
    }
}