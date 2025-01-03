using CatalogoFilmesSeries.Domain.Entities;
using NUnit.Framework.Legacy;

namespace CatalogoFilmesSeries.Tests;

public class CadastroFilmesTests
{
 
    
    [Test]
    public void Deve_Criar_Um_Filme_Com_Sucesso()
    {
        //Arrange
        List<string> categorias = ["One-person Army action", "SuperHero", "Action", "Thriller"];
        //Act
        
        Filme filme = Filme.Create("Kraven, o Caçador", "Kraven the Hunter", 2024, 16, 127, 
            "A complexa relação de Kraven com o pai, Nikolai Kravinoff, o leva a uma jornada de vingança com consequências brutais, o motivando a se tornar um dos maiores e mais temidos caçadores do mundo.", 
            5.6, 24, "https://www.imdb.com/title/tt8790086/mediaviewer/rm1284204801/?ref_=tt_ov_i"); 
        
        filme.AddCategorias(categorias);
        
        //Assert
        Assert.That(filme, Is.InstanceOf<Filme>());
        Assert.That(filme.Id, Is.InstanceOf<Guid>());
        Assert.That(filme.Categorias.Count, Is.EqualTo(categorias.Count));
    }
    
    [Test]
    public void Deve_Atualizar_Um_Filme_Com_Sucesso()
    {
        //Arrange
        List<string> categorias = ["One-person Army action", "SuperHero", "Action", "Thriller"];
        //Act
        
        Filme filme = Filme.Create("Kraven, o Caçador", "Kraven the Hunter", 2024, 16, 127, 
            "A complexa relação de Kraven com o pai, Nikolai Kravinoff, o leva a uma jornada de vingança com consequências brutais, o motivando a se tornar um dos maiores e mais temidos caçadores do mundo.", 
            5.6, 24, "https://www.imdb.com/title/tt8790086/mediaviewer/rm1284204801/?ref_=tt_ov_i"); 
        
        filme.AddCategorias(categorias);
        
        //Assert
        Assert.That(filme, Is.InstanceOf<Filme>());
        Assert.That(filme.Id, Is.InstanceOf<Guid>());
        Assert.That(filme.Categorias.Count, Is.EqualTo(categorias.Count));
    }
}