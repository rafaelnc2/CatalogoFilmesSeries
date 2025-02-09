namespace CatalogoFilmesSeries.Application.UseCases.Series.Adicionar;

public sealed record AdicionarResponse(
    string Titulo,
    string TtuloOriginal,
    int AnoLancamento,
    int ClassificacaoEtaria,
    string Sinopse,
    string UrlImagem,

    double AvaliacaoImdb,
    double PopularidadeImdb,
    int QuantidadeVotosImdb,

    int Termporada,
    int QuantidadeEpisodios,
    double DuracaoEpisodios,

    DateTime DataInclusao
)
{
    public static explicit operator AdicionarResponse(Serie serie) =>
        new AdicionarResponse(
            serie.Titulo, 
            serie.TituloOriginal, 
            serie.AnoLancamento, 
            serie.ClassificacaoEtaria, 
            serie.Sinopse, 
            serie.UrlImagem, 
            serie.ImdbInfo.VoteAverage, 
            serie.ImdbInfo.Popularity, 
            serie.ImdbInfo.VoteCount,
            
            serie.Temporadas,
            serie.QuantidadeEpisodios,
            serie.DuracaoEpisodios,
            
            serie.DataInclusao
        );
};