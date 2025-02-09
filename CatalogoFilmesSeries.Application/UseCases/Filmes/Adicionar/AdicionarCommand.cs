namespace CatalogoFilmesSeries.Application.UseCases.Filmes.Adicionar;

public sealed record AdicionarCommand(
    string Titulo,
    string TituloOriginal,
    int AnoLancamento,
    int ClassificacaoEtaria,
    int Duracao,
    string Sinopse,
    string UrlImagem
) : IRequest<ApiResult<AdicionarResponse>>
{
    public IEnumerable<string> Categorias { get; private set; } = Enumerable.Empty<string>();
}