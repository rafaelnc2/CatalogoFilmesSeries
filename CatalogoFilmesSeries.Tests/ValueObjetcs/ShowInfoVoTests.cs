using CatalogoFilmesSeries.Domain.ValueObjects;

namespace CatalogoFilmesSeries.Unit.Tests.ValueObjetcs;

public class ShowInfoVoTests
{
    [Fact(DisplayName = "Deve criar uma nova instância corretamente")]
    public void ImdbInfoVo_Deve_Criar_Uma_Instancia_Com_Sucesso()
    {
        //Arrange
        double? popularity = 10;
        double? voteAverage = 50;
        int? voteCount = 100;

        //Act
        ShowInfoVo showInfoVo = new(popularity, voteAverage, voteCount); 

        //Assert
        Assert.IsType<ShowInfoVo>(showInfoVo);
        Assert.False(showInfoVo.HasErrors);
        
        Assert.Equal(popularity, showInfoVo.Popularity);
        Assert.Equal(voteAverage, showInfoVo.VoteAverage);
        Assert.Equal(voteCount, showInfoVo.VoteCount);
    }
    
    [Fact(DisplayName = "Deve criar uma nova instância com os valores iguais a zero quando parâmetros forem nulos")]
    public void ImdbInfoVo_Deve_Criar_Uma_Instancia_Com_Valores_Zerados_Quando_Parametros_Sao_nulos()
    {
        //Arrange
        double? popularity = null;
        double? voteAverage = null;
        int? voteCount = null;

        //Act
        ShowInfoVo showInfoVo = new(popularity, voteAverage, voteCount); 

        //Assert
        Assert.IsType<ShowInfoVo>(showInfoVo);
        Assert.False(showInfoVo.HasErrors);
        
        Assert.Equal(0, showInfoVo.Popularity);
        Assert.Equal(0, showInfoVo.VoteAverage);
        Assert.Equal(0, showInfoVo.VoteCount);
    }
}