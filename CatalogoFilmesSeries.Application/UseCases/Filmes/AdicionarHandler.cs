using CatalogoFilmesSeries.Application.Services;

namespace CatalogoFilmesSeries.Application.UseCases.Filmes;

public sealed class AdicionarHandler : IRequestHandler<AdicionarCommand, ApiResult<AdicionarResponse>>
{
    private readonly IExternalShowInfoService _externalShowInfoService;

    public AdicionarHandler(IExternalShowInfoService externalShowInfoService)
    {
        _externalShowInfoService = externalShowInfoService;
    }

    public async Task<ApiResult<AdicionarResponse>> Handle(AdicionarCommand command, CancellationToken cancellationToken)
    {
        var filme = Filme.Create(command.Titulo, command.TituloOriginal, command.AnoLancamento,
            command.ClassificacaoEtaria, command.Duracao, command.Sinopse, command.UrlImagem);
        
        if(filme.HasErrors)
            return ApiResult<AdicionarResponse>.BadRequest("Dados inválidos para a inclusão de um Filme", filme.Errors);

        var avaliacaoImdb = await _externalShowInfoService.GetAvaliacaoImdbAsync(filme.TituloOriginal);
        
        var popularidadeImdb = await _externalShowInfoService.GetPopularidadeImdbAsync(filme.TituloOriginal);
        
        filme.SetAvaliacaoImdb(avaliacaoImdb);
        
        filme.SetPopularidadeImdb(popularidadeImdb);
        
        var response = new AdicionarResponse(
            filme.Titulo, 
            filme.TituloOriginal, 
            filme.AnoLancamento, 
            filme.ClassificacaoEtaria, 
            filme.Duracao, 
            filme.Sinopse, 
            filme.UrlImagem, 
            filme.AvaliacaoImdb, 
            filme.PopularidadeImdb, 
            filme.DataInclusao
        );

        return ApiResult<AdicionarResponse>.Created(response);
    }
}