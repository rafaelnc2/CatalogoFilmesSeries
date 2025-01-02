namespace CatalogoFilmesSeries.Domain.Entities;

public class Filme : Entity
{
    private List<string> _categorias = new();

    private Filme()
    {
        
    }
    
    protected Filme(Guid id, string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria, 
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
    
    public string Titulo { get; private set; }
    public string TituloOriginal { get; private set; }
    public int AnoLancamento { get; private set; }
    public int ClassificacaoEtaria { get; private set; }
    public int Duracao { get; private set; }
    public string Sinopse { get; private set; }
    public IReadOnlyList<string> Categorias { get => _categorias; }
    public string UrlImagem { get; private set; }

    
    public double AvaliacaoImdb { get; private set; }
    public int PopularidadeImdb { get; private set; }


    public void SetAvaliacaoImdb(double avaliacao)
    {
        if (avaliacao < 0)
        {
            _errors.Add("Avaliação informada é inválida");
            return;
        }
        
        AvaliacaoImdb = avaliacao;   
    }

    public void SetPopularidadeImdb(int popularidade)
    {
        if (popularidade < 0)
        {
            _errors.Add("Popularidade informada é inválida");
            return;
        }
        
        PopularidadeImdb = popularidade;
    }

    public static Filme? Create(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
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
        int duracao, string sinopse, double avaliacaoImdb, int popularidadeImdb, string urlImagem)
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
        AvaliacaoImdb = avaliacaoImdb;
        PopularidadeImdb = popularidadeImdb;
        UrlImagem = urlImagem;
        
        DataAtualizacao = DateTime.Now;
    }

    public void AddCategoria(string categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria) ||
            _categorias.Exists(cat => cat.Equals(categoria, StringComparison.CurrentCultureIgnoreCase)))
        {
            _errors.Add("Categoria nula, em branco ou já incluída.");
            return;
        }
        
        _categorias.Add(categoria);
    }
    public void RemoveCategoria(string categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria))
            return;
        
        _categorias.Remove(categoria);  
    } 
    public void LimparCategorias() => _categorias.Clear();

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