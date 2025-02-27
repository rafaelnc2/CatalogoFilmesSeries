using CatalogoFilmesSeries.Application.UseCases.Filmes.Adicionar;

namespace CatalogoFilesSeries.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    private readonly ILogger<FilmesController> _logger;
    private readonly IMediator _mediator;

    public FilmesController(ILogger<FilmesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    

    [HttpPost(Name = "AdicionarFilme")]
    public async Task<ApiResult<AdicionarResponse>> Post(AdicionarCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
}