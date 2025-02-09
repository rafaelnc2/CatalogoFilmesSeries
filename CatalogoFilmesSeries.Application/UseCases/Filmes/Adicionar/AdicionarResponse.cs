namespace CatalogoFilmesSeries.Application.UseCases.Filmes.Adicionar;

public sealed record AdicionarResponse(
    string Titulo,
    string TtuloOriginal,
    int AnoLancamento,
    int ClassificacaoEtaria,
    int Duracao,
    string Sinopse,
    string UrlImagem,

    double AvaliacaoImdb,
    double PopularidadeImdb,
    int QuantidadeVotosImdb,

    DateTime DataInclusao
)
{
    public static explicit operator AdicionarResponse(Filme filme) =>
        new AdicionarResponse(
            filme.Titulo, 
            filme.TituloOriginal, 
            filme.AnoLancamento, 
            filme.ClassificacaoEtaria, 
            filme.Duracao, 
            filme.Sinopse, 
            filme.UrlImagem, 
            filme.ImdbInfo.VoteAverage, 
            filme.ImdbInfo.Popularity, 
            filme.ImdbInfo.VoteCount,
            filme.DataInclusao
        );
};