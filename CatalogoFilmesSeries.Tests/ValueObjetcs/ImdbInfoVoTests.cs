using CatalogoFilmesSeries.Domain.ValueObjects;

namespace CatalogoFilmesSeries.Unit.Tests.ValueObjetcs;

public class ImdbInfoVoTests
{
    [Fact(DisplayName = "Deve criar uma nova instância corretamente")]
    public void ImdbInfoVo_Deve_Criar_Uma_Instancia_Com_Sucesso()
    {
        //Arrange
        double? popularity = 10;
        double? voteAverage = 50;
        int? voteCount = 100;

        //Act
        ImdbInfoVo imdbInfoVo = new(popularity, voteAverage, voteCount); 

        //Assert
        Assert.IsType<ImdbInfoVo>(imdbInfoVo);
        Assert.False(imdbInfoVo.HasErrors);
        
        Assert.Equal(popularity, imdbInfoVo.Popularity);
        Assert.Equal(voteAverage, imdbInfoVo.VoteAverage);
        Assert.Equal(voteCount, imdbInfoVo.VoteCount);
    }
    
    [Fact(DisplayName = "Deve criar uma nova instância com os valores iguais a zero quando parâmetros forem nulos")]
    public void ImdbInfoVo_Deve_Criar_Uma_Instancia_Com_Valores_Zerados_Quando_Parametros_Sao_nulos()
    {
        //Arrange
        double? popularity = null;
        double? voteAverage = null;
        int? voteCount = null;

        //Act
        ImdbInfoVo imdbInfoVo = new(popularity, voteAverage, voteCount); 

        //Assert
        Assert.IsType<ImdbInfoVo>(imdbInfoVo);
        Assert.False(imdbInfoVo.HasErrors);
        
        Assert.Equal(0, imdbInfoVo.Popularity);
        Assert.Equal(0, imdbInfoVo.VoteAverage);
        Assert.Equal(0, imdbInfoVo.VoteCount);
    }
}