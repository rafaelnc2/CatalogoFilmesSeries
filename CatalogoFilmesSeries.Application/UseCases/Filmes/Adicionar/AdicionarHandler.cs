using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Filmes;

namespace CatalogoFilmesSeries.Application.UseCases.Filmes.Adicionar;

public sealed class AdicionarHandler : IRequestHandler<AdicionarCommand, ApiResult<AdicionarResponse>>
{
    private readonly ILogger<AdicionarHandler> _logger;
    
    private readonly IShowInfoService _showInfoService;
    private readonly IFilmeWriteRepository _filmeWriteRepository;
    private readonly IFilmeReadRepository _filmeReadRepository;
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public AdicionarHandler(ILogger<AdicionarHandler> logger, 
        IShowInfoService showInfoService, 
        IFilmeWriteRepository filmeWriteRepository, IFilmeReadRepository filmeReadRepository, 
        IIntegrationEventPublisher integrationEventPublisher)
    {
        _logger = logger;
        
        _showInfoService = showInfoService;
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
        
        ShowInfoVo showInfo = await _showInfoService.GetFilmeImdbInfoAsync(command.Titulo, command.AnoLancamento, cancellationToken);
        
        var filme = Filme.Create(
            command.Titulo, 
            command.TituloOriginal, 
            command.AnoLancamento,
            command.ClassificacaoEtaria, 
            command.Duracao, 
            command.Sinopse, 
            command.UrlImagem,
            showInfo
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
    
    
    private async Task PublishEventsAsync(Filme filme)
    {
        _logger.LogInformation("Criando evento!");
        
        var filmeCriadoIntegrationEvent = new FilmeCriadoIntegrationEvent(filme);
        
        await _integrationEventPublisher.PublishAsync(filmeCriadoIntegrationEvent);
        
        _logger.LogInformation("Evento publicado com sucesso!");
    }
}