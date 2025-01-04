using System.Dynamic;

namespace CatalogoFilmesSeries.Domain.Entities;

public sealed class Serie : Show
{
    public int QuantidadeEpisodios { get; private set; }
    public double DuracaoEpisodios { get; private set; }

    private Serie()
    {
        
    }
    
    private Serie(Guid id, string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria, 
        string sinopse, List<string> categorias, string urlImagem, int quantidadeEpisodios, double duracaoEpisodios,
        DateTime dataInclusao, DateTime? dataAtualizacao, double avaliacaoImdb = 0, int popularidadeImdb = 0)
    {
        Id = id;
        Titulo = titulo;
        TituloOriginal = tituloOriginal;
        AnoLancamento = anoLancamento;
        ClassificacaoEtaria = classificacaoEtaria;
        Sinopse = sinopse;
        UrlImagem = urlImagem;
        
        QuantidadeEpisodios = quantidadeEpisodios;
        DuracaoEpisodios = duracaoEpisodios;
        
        DataInclusao = dataInclusao;
        DataAtualizacao = dataAtualizacao;
        
        AvaliacaoImdb = avaliacaoImdb;
        PopularidadeImdb = popularidadeImdb;
        
        _categorias = categorias;
    }

    public static Serie Create(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
        string sinopse, string urlImagem, int quantidadeEpisodios, double duracaoEpisodios)
    {
        var serieValidate = new Serie();
        
        serieValidate.ValidarDados(titulo, tituloOriginal, anoLancamento, classificacaoEtaria, sinopse, urlImagem, quantidadeEpisodios, duracaoEpisodios);

        if (serieValidate.HasErrors)
            return serieValidate;

        Serie serie = new(
            id: Guid.NewGuid(),
            titulo: titulo,
            tituloOriginal: tituloOriginal,
            anoLancamento: anoLancamento,
            classificacaoEtaria: classificacaoEtaria,
            sinopse: sinopse,
            categorias: [],
            urlImagem: urlImagem,
            quantidadeEpisodios: quantidadeEpisodios,
            duracaoEpisodios: duracaoEpisodios,
            dataInclusao: DateTime.Now,
            dataAtualizacao: null
        );

        return serie;
    }
        
    public void Update(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
        string sinopse, double? avaliacaoImdb, int? popularidadeImdb, string urlImagem, int quantidadeEpisodios, double duracaoEpisodios)
    {
        ValidarDados(titulo, tituloOriginal, anoLancamento, classificacaoEtaria, sinopse, urlImagem, quantidadeEpisodios, duracaoEpisodios);
        
        if(HasErrors)
            return;
        
        Titulo = titulo;
        TituloOriginal = tituloOriginal;
        AnoLancamento = anoLancamento;
        ClassificacaoEtaria = classificacaoEtaria;
        Sinopse = sinopse;
        
        if(avaliacaoImdb.HasValue)
            AvaliacaoImdb = avaliacaoImdb.Value;
        
        if(popularidadeImdb.HasValue)
            PopularidadeImdb = popularidadeImdb.Value;
        
        UrlImagem = urlImagem;
        
        QuantidadeEpisodios = quantidadeEpisodios;
        DuracaoEpisodios = duracaoEpisodios;
        
        DataAtualizacao = DateTime.Now;
    }
    
    private void ValidarDados(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
        string sinopse, string urlImagem, int quantidadeEpisodios, double duracaoEpisodios)
    {
        _errors.Clear();
        
        if (string.IsNullOrWhiteSpace(titulo) || titulo.Length < 3)
            _errors.Add("Titulo inválido");
        
        if (string.IsNullOrWhiteSpace(tituloOriginal) || tituloOriginal.Length < 3)
            _errors.Add("Titulo original inválido");
        
        if (anoLancamento <= 0)
            _errors.Add("Ano de lançamento inválido");
        
        if (classificacaoEtaria <= 0)
            _errors.Add("Classificação etária inválida");
        
        if (quantidadeEpisodios <= 0)
            _errors.Add("Quantidade de episódios inválida");
        
        if (duracaoEpisodios <= 0)
            _errors.Add("Duração dos episódios inválida");
        
        if (string.IsNullOrWhiteSpace(sinopse) || sinopse.Length < 3)
            _errors.Add("Sinopse inválida");
        
        if (string.IsNullOrWhiteSpace(urlImagem) || Uri.IsWellFormedUriString(urlImagem, UriKind.Absolute) is false)
            _errors.Add("URL da imagem inválida");
    }
}