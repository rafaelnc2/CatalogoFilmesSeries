using CatalogoFilmesSeries.Domain.Entities;
using CatalogoFilmesSeries.Domain.ValueObjects;

namespace CatalogoFilmesSeries.Unit.Tests.Entities;

public class ShowTests
{
    private readonly Filme _filme;

    public ShowTests()
    {
        List<string> categorias = ["One-person Army action", "SuperHero", "Action", "Thriller"];

        ShowInfoVo showInfoMock = new(1, 1, 1);
        
        _filme = Filme.Create(
            "Kraven, o Caçador", 
            "Kraven the Hunter", 
            2024, 
            16, 
            127, 
            "A complexa relação de Kraven com o pai, Nikolai Kravinoff, o leva a uma jornada de vingança com consequências brutais, o motivando a se tornar um dos maiores e mais temidos caçadores do mundo.", 
            "https://www.imdb.com/title/tt8790086/mediaviewer/rm1284204801/?ref_=tt_ov_i",
            showInfoMock
        );

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
}