using CatalogoFilmesSeries.Domain.Entities;
using CatalogoFilmesSeries.Domain.ValueObjects;

namespace CatalogoFilmesSeries.Unit.Tests.Entities;

public class SerieTests
{
    private readonly Serie _serie;

    public SerieTests()
    {
        List<string> categorias = ["One-person Army action", "SuperHero", "Action", "Thriller"];
        
        ShowInfoVo showInfoMock = new(10, 50, 100);
        
        _serie = Serie.Create(
            "Kraven, o Caçador", 
            "Kraven the Hunter",
            2024,
            16,
            "A complexa relação de Kraven com o pai, Nikolai Kravinoff, o leva a uma jornada de vingança com consequências brutais, o motivando a se tornar um dos maiores e mais temidos caçadores do mundo.", 
            "https://www.imdb.com/title/tt8790086/mediaviewer/rm1284204801/?ref_=tt_ov_i",
            1,
            10,
            35,
            showInfoMock
        );

        foreach (var categoria in categorias)
            _serie.AddCategoria(categoria);
    }
    
    [Fact(DisplayName = "Criar nova série com sucesso")]
    public void Criar_Serie_Com_Sucesso()
    {
        //Arrange
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now.Date);
        
        //Assert
        Assert.IsType<Serie>(_serie);
        Assert.False(_serie.HasErrors);
        Assert.IsType<Guid>(_serie.Id);
        Assert.False(_serie.Id.Equals(Guid.Empty));
        Assert.Equal(currentDate, DateOnly.FromDateTime(_serie.DataInclusao));
        Assert.Null(_serie.DataAtualizacao);
        Assert.Equal(_serie.Categorias.Count, _serie.Categorias.Count);
    }
    
    [Fact(DisplayName = "Retornar erro ao tentar criar nova série com dados inválidos")]
    public void Retornar_Erro_Ao_Tentar_Criar_Serie_Com_Dados_Invalidos()
    {
        //Arrange
        string tituloInvalido = "xx";
        string tituloOriginalInvalido = "xx";
        int anoLancamentoInvalido = 0;
        int classificacaoInvalida = 0;
        string sinopseInvalida = "";
        string urlInvalida = "url-invalida";
        int temporadaInvalido = 0;
        int quantidadeEpisodiosInvalida = 0;
        double duracaoInvalida = 0;
        
        ShowInfoVo showInfoMock = new(10, 50, 100);
        
        //Act
        var serie = Serie.Create(
            tituloInvalido, 
            tituloOriginalInvalido, 
            anoLancamentoInvalido,
            classificacaoInvalida,
            sinopseInvalida,
            urlInvalida,
            temporadaInvalido,
            quantidadeEpisodiosInvalida,
            duracaoInvalida,
            showInfoMock
        );
        
        //Assert
        Assert.IsType<Serie>(serie);
        Assert.IsType<Guid>(serie.Id);
        Assert.Equal(Guid.Empty, serie.Id);
        Assert.True(serie.HasErrors);
        Assert.Equal(9, serie.Errors.Count);
    }
    
    [Fact(DisplayName = "Atualizar série com sucesso")]
    public void Atualizar_Serie_Com_Sucesso()
    {
        //Arrange
        string tituloAtualizado = "Kraven, o Caçador";
        string tituloOriginalAtualizado = "Kraven the Hunter";
        string categoriaParaRemover = "Thriller";
        int qtdCategoriasAtualizada = 3;
        
        ShowInfoVo showInfoMock = new(10, 50, 100);

        //Act
        _serie.Update(
            tituloAtualizado,
            tituloOriginalAtualizado,
            2024,
            16,
            "A complexa relação de Kraven com o pai, Nikolai Kravinoff, o leva a uma jornada de vingança com consequências brutais, o motivando a se tornar um dos maiores e mais temidos caçadores do mundo.",
            5.6,
            24,
            "https://www.imdb.com/title/tt8790086/mediaviewer/rm1284204801/?ref_=tt_ov_i",
            1,
            10, 
            showInfoMock,
            10
        );
        
        _serie.RemoveCategoria(categoriaParaRemover);

        //Assert
        Assert.False(_serie.HasErrors); 
        Assert.Equal(tituloAtualizado, _serie.Titulo);
        Assert.Equal(tituloOriginalAtualizado, _serie.TituloOriginal);
        Assert.Equal(qtdCategoriasAtualizada, _serie.Categorias.Count);
        Assert.NotNull(_serie.DataAtualizacao);
    }
    
    [Fact(DisplayName = "Retornar erro ao tentar atualizar série com dados inválidos")]
    public void Retornar_Erro_Ao_Tentar_Atualizar_Serie_Com_Dados_Invalidos()
    {
        //Arrange
        string tituloInvalido = "xx";
        string tituloOriginalInvalido = "xx";
        int anoLancamentoInvalido = 0;
        int classificacaoInvalida = 0;
        string sinopseInvalida = "";
        string urlInvalida = "url-invalida";
        int temporadaInvalida = -1;
        int quantidadeEpisodiosInvalida = -1;
        double duracaoEpisodiosInvalida = -1;
        
        ShowInfoVo showInfoMock = new(10, 50, 100);
        
        //Act
        _serie.Update(
            tituloInvalido,
            tituloOriginalInvalido,
            anoLancamentoInvalido,
            classificacaoInvalida,
            sinopseInvalida,
            0,
            0,
            urlInvalida,
            temporadaInvalida,
            quantidadeEpisodiosInvalida,
            showInfoMock,
            duracaoEpisodiosInvalida
        );
        
        //Assert
        Assert.True(_serie.HasErrors);
        Assert.Equal(9, _serie.Errors.Count);
    }
}