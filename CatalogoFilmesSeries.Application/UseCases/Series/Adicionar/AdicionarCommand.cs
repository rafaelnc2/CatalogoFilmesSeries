namespace CatalogoFilmesSeries.Application.UseCases.Series.Adicionar;

public sealed record AdicionarCommand(
    string Titulo,
    string TituloOriginal,
    int AnoLancamento,
    int ClassificacaoEtaria,
    string Sinopse,
    string UrlImagem,
    
    int Termporadas,
    int QuantidadeEpisodios,
    double DuracaoEpisodios
) : IRequest<ApiResult<AdicionarResponse>>
{
    public IEnumerable<string> Categorias { get; private set; } = Enumerable.Empty<string>();
}