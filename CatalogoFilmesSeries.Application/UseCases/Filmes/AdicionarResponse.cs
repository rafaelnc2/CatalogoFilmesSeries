namespace CatalogoFilmesSeries.Application.UseCases.Filmes;

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
    
    DateTime DataInclusao
);