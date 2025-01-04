using CatalogoFilmesSeries.Domain.Entities;

namespace CatalogoFilmesSeries.Unit.Tests.Entities;

public class ShowTests
{
    private readonly Filme _filme;

    public ShowTests()
    {
        List<string> categorias = ["One-person Army action", "SuperHero", "Action", "Thriller"];
        
        _filme = Filme.Create("Kraven, o Caçador", "Kraven the Hunter", 2024, 16, 127, 
            "A complexa relação de Kraven com o pai, Nikolai Kravinoff, o leva a uma jornada de vingança com consequências brutais, o motivando a se tornar um dos maiores e mais temidos caçadores do mundo.", 
            "https://www.imdb.com/title/tt8790086/mediaviewer/rm1284204801/?ref_=tt_ov_i");

        foreach (var categoria in categorias)
            _filme.AddCategoria(categoria);
    }
    
    [Theory(DisplayName = "Não deve incluir categorias inválidas")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Nao_Deve_Incluir_Categorias_Invalidas(string? categoriaInvalida)
    {
        //Arrange
        _filme.LimparCategorias(); 
        
        //Act
        _filme.AddCategoria(categoriaInvalida);
        
        //Assert
        Assert.True(_filme.HasErrors);
        Assert.Equal("Categoria nula, em branco ou já incluída.", _filme.Errors.First());
    }
    
    [Theory(DisplayName = "Não deve incluir categorias repetidas")]
    [InlineData("Action")]
    [InlineData("SuperHero")]
    public void Nao_Deve_Incluir_Categorias_Repetidas(string categoriaDuplicada)
    {
        //Act
        _filme.AddCategoria(categoriaDuplicada);
        
        //Assert
        Assert.True(_filme.HasErrors);
        Assert.Equal("Categoria nula, em branco ou já incluída.", _filme.Errors.First());
    }
    
    [Fact(DisplayName = "Deve atualizar a avalicaoImdb com sucesso")]
    public void Deve_Atualizar_A_AvalicaoImdb_Com_Sucesso()
    {
        //Arrange
        double avaliacaoImdb = 10;
        
        //Act
        _filme.SetAvaliacaoImdb(avaliacaoImdb);
        
        //Assert
        Assert.False(_filme.HasErrors);
        Assert.Equal(avaliacaoImdb, _filme.AvaliacaoImdb);
    }
    
    [Fact(DisplayName = "Deve retornar erro ao atualizar a avalicaoImdb com valor iInvalido")]
    public void Deve_Retornar_Erro_Ao_Atualizar_A_AvalicaoImdb_Com_Valor_Invalido()
    {
        //Arrange
        double avaliacaoImdb = -1;
        
        //Act
        _filme.SetAvaliacaoImdb(avaliacaoImdb);
        
        //Assert
        Assert.True(_filme.HasErrors);
        Assert.Equal("Avaliação informada é inválida", _filme.Errors.First());
        Assert.Equal(0, _filme.AvaliacaoImdb);
    }
    
    [Fact(DisplayName = "Deve atualizar a popularidade Imdb com sucesso")]
    public void Deve_Atualizar_A_PopularidadeImdb_Com_Sucesso()
    {
        //Arrange
        int popularidadeImdb = 100;
        
        //Act
        _filme.SetPopularidadeImdb(popularidadeImdb);
        
        //Assert
        Assert.False(_filme.HasErrors);
        Assert.Equal(popularidadeImdb, _filme.PopularidadeImdb);
    }
    
    [Fact(DisplayName = "Deve retornar erro ao atualizar a popularidade Imdb com valor iInvalido")]
    public void Deve_Retornar_Erro_Ao_Atualizar_A_PopularidadeImdb_Com_Valor_Invalido()
    {
        //Arrange
        int popularidadeImdb = -1;
        
        //Act
        _filme.SetPopularidadeImdb(popularidadeImdb);
        
        //Assert
        Assert.True(_filme.HasErrors);
        Assert.Equal("Popularidade informada é inválida", _filme.Errors.First());
        Assert.Equal(0, _filme.PopularidadeImdb);
    }
}