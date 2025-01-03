namespace CatalogoFilmesSeries.Domain.Entities;

public sealed class Filme : Show
{
    public int Duracao { get; private set; }
    
    private Filme()
    {
        
    }
    
    private Filme(Guid id, string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria, 
        int duracao, string sinopse, List<string> categorias, string urlImagem, DateTime dataInclusao, DateTime? dataAtualizacao,
        double avaliacaoImdb = 0, int popularidadeImdb = 0)
    {
        Id = id;
        Titulo = titulo;
        TituloOriginal = tituloOriginal;
        AnoLancamento = anoLancamento;
        ClassificacaoEtaria = classificacaoEtaria;
        Duracao = duracao;
        Sinopse = sinopse;
        UrlImagem = urlImagem;
        DataInclusao = dataInclusao;
        DataAtualizacao = dataAtualizacao;
        
        AvaliacaoImdb = avaliacaoImdb;
        PopularidadeImdb = popularidadeImdb;
        
        _categorias = categorias;
    }

    public static Filme Create(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
        int duracao, string sinopse, string urlImagem)
    {
        _errors.Clear();

        ValidarDados(titulo, tituloOriginal, anoLancamento, classificacaoEtaria, duracao, sinopse, urlImagem);

        if (_errors.Any())
            return new();
        
        Filme filme = new(
            id: Guid.NewGuid(),
            titulo: titulo,
            tituloOriginal: tituloOriginal,
            anoLancamento: anoLancamento,
            classificacaoEtaria: classificacaoEtaria,
            duracao: duracao,
            sinopse: sinopse,
            categorias: [],
            urlImagem: urlImagem,
            dataInclusao: DateTime.Now,
            dataAtualizacao: null
        );
        
        return filme;
    }
    
    public void Update(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
        int duracao, string sinopse, double? avaliacaoImdb, int? popularidadeImdb, string urlImagem)
    {
        _errors.Clear();
        
        ValidarDados(titulo, tituloOriginal, anoLancamento, classificacaoEtaria, duracao, sinopse, urlImagem);
        
        if(HasErrors)
            return;
        
        Titulo = titulo;
        TituloOriginal = tituloOriginal;
        AnoLancamento = anoLancamento;
        ClassificacaoEtaria = classificacaoEtaria;
        Duracao = duracao;
        Sinopse = sinopse;
        
        if(avaliacaoImdb.HasValue)
            AvaliacaoImdb = avaliacaoImdb.Value;
        
        if(popularidadeImdb.HasValue)
            PopularidadeImdb = popularidadeImdb.Value;
        
        UrlImagem = urlImagem;
        
        DataAtualizacao = DateTime.Now;
    }
    

    private static void ValidarDados(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
        int duracao, string sinopse, string urlImagem)
    {
        if (string.IsNullOrWhiteSpace(titulo) || titulo.Length < 3)
            _errors.Add("Titulo inválido");
        
        if (string.IsNullOrWhiteSpace(tituloOriginal) || tituloOriginal.Length < 3)
            _errors.Add("Titulo original inválido");
        
        if (anoLancamento <= 0)
            _errors.Add("Ano de lançamento inválido");
        
        if (classificacaoEtaria <= 0)
            _errors.Add("Classificação etária inválida");
        
        if (duracao <= 0)
            _errors.Add("Tempo de duração inválido");
        
        if (string.IsNullOrWhiteSpace(sinopse) || sinopse.Length < 3)
            _errors.Add("Sinopse inválida");
        
        if (string.IsNullOrWhiteSpace(urlImagem) || Uri.IsWellFormedUriString(urlImagem, UriKind.Absolute) is false)
            _errors.Add("URL da imagem inválida");
    }
}