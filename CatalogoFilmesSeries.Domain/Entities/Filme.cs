using CatalogoFilmesSeries.Domain.ValueObjects;

namespace CatalogoFilmesSeries.Domain.Entities;

public sealed class Filme : Show
{
    public int Duracao { get; private set; }
    
    private Filme()
    {
        
    }
    
    private Filme(Guid id, string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria, 
        int duracao, string sinopse, List<string> categorias, string urlImagem, ShowInfoVo showInfo, 
        DateTime dataInclusao, DateTime? dataAtualizacao)
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
        
        ShowInfo = showInfo;
        
        _categorias = categorias;
    }

    public static Filme Create(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
        int duracao, string sinopse, string urlImagem, ShowInfoVo showInfo)
    {
        var filmeValidate = new Filme();
        
        filmeValidate.ValidarDados(titulo, tituloOriginal, anoLancamento, classificacaoEtaria, duracao, sinopse, urlImagem);

        if (filmeValidate.HasErrors)
            return filmeValidate;
        
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
            showInfo: showInfo,
            dataInclusao: DateTime.Now,
            dataAtualizacao: null
        );
        
        return filme;
    }
    
    public void Update(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
        int duracao, string sinopse, string urlImagem, ShowInfoVo showInfo)
    {
        ValidarDados(titulo, tituloOriginal, anoLancamento, classificacaoEtaria, duracao, sinopse, urlImagem);
        
        if(HasErrors)
            return;
        
        Titulo = titulo;
        TituloOriginal = tituloOriginal;
        AnoLancamento = anoLancamento;
        ClassificacaoEtaria = classificacaoEtaria;
        Duracao = duracao;
        Sinopse = sinopse;
        
        ShowInfo = showInfo;
        
        UrlImagem = urlImagem;
        
        DataAtualizacao = DateTime.Now;
    }
    

    private void ValidarDados(string titulo, string tituloOriginal, int anoLancamento, int classificacaoEtaria,
        int duracao, string sinopse, string urlImagem)
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
        
        if (duracao <= 0)
            _errors.Add("Tempo de duração inválido");
        
        if (string.IsNullOrWhiteSpace(sinopse) || sinopse.Length < 3)
            _errors.Add("Sinopse inválida");
        
        if (string.IsNullOrWhiteSpace(urlImagem) || Uri.IsWellFormedUriString(urlImagem, UriKind.Absolute) is false)
            _errors.Add("URL da imagem inválida");
    }
}