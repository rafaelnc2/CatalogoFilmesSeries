using CatalogoFilmesSeries.Application.Interfaces.Repositories.Filmes;
using CatalogoFilmesSeries.Domain.ValueObjects;

namespace CatalogoFilmesSeries.Application.UseCases.Filmes.Adicionar;

public sealed class AdicionarHandler : IRequestHandler<AdicionarCommand, ApiResult<AdicionarResponse>>
{
    private readonly ILogger<AdicionarHandler> _logger;
    
    private readonly IExternalShowInfoService _externalShowInfoService;
    private readonly IFilmeWriteRepository _filmeWriteRepository;
    private readonly IFilmeReadRepository _filmeReadRepository;
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public AdicionarHandler(ILogger<AdicionarHandler> logger, 
        IExternalShowInfoService externalShowInfoService, 
        IFilmeWriteRepository filmeWriteRepository, IFilmeReadRepository filmeReadRepository, 
        IIntegrationEventPublisher integrationEventPublisher)
    {
        _logger = logger;
        
        _externalShowInfoService = externalShowInfoService;
        _filmeWriteRepository = filmeWriteRepository;
        _filmeReadRepository = filmeReadRepository;
        _integrationEventPublisher = integrationEventPublisher;
    }

    public async Task<ApiResult<AdicionarResponse>> Handle(AdicionarCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hanlder para criação de um novo Filme!");
        
        var movieExists = await _filmeReadRepository.SearchByNameAsync(command.Titulo);

        if (movieExists is not null)
        {
            _logger.LogWarning("Titulo informado já existe");
            return ApiResult<AdicionarResponse>.BadRequest($"Titulo informado já existe com ID {movieExists.Id}");
        }
        
        ImdbInfoVo imdbInfo = await GetImdbInfoAsync(command.Titulo);
        
        var filme = Filme.Create(
            command.Titulo, 
            command.TituloOriginal, 
            command.AnoLancamento,
            command.ClassificacaoEtaria, 
            command.Duracao, 
            command.Sinopse, 
            command.UrlImagem,
            imdbInfo
        );

        if (filme.HasErrors)
        {
            _logger.LogWarning("Dados inválidos para a inclusão de um Filme");
            return ApiResult<AdicionarResponse>.BadRequest("Dados inválidos para a inclusão de um Filme", filme.Errors);
        }
        
        await _filmeWriteRepository.AddAsync(filme);
        
        _logger.LogInformation("Filme criado com sucesso!");

        await PublishEventsAsync(filme);

        var response = (AdicionarResponse)filme;
        
        return ApiResult<AdicionarResponse>.Created(response);
    }

    private async Task<ImdbInfoVo> GetImdbInfoAsync(string titulo)
    {
        var imdbInfoDto = await _externalShowInfoService.GetImdbDataAsync(titulo);

        return new(imdbInfoDto?.Popularity, imdbInfoDto?.VoteAverage, imdbInfoDto?.VoteCount);
    } 
    
    private async Task PublishEventsAsync(Filme filme)
    {
        _logger.LogInformation("Criando evento!");
        
        var filmeCriadoIntegrationEvent = new FilmeCriadoIntegrationEvent(filme);
        
        await _integrationEventPublisher.PublishAsync(filmeCriadoIntegrationEvent);
        
        _logger.LogInformation("Evento publicado com sucesso!");
    }
}