using CatalogoFilmesSeries.Application.Services;
using CatalogoFilmesSeries.Application.Shared;
using CatalogoFilmesSeries.Application.UseCases.Filmes;
using Moq;

namespace CatalogoFilmesSeries.Unit.Tests.Handlers.Filme;

public class AdicionarHandlerTests
{
    [Fact]
    public async Task AdicionarHandler_Deve_Adicionar_Um_Novo_Filme_Com_Sucesso()
    {
        //Arrange
        var commandMock = new AdicionarCommand(
            "Kraven, o Caçador",
            "Kraven the Hunter",
            2024,
            16,
            127,
            "A complexa relação de Kraven com o pai, Nikolai Kravinoff, o leva a uma jornada de vingança com consequências brutais, o motivando a se tornar um dos maiores e mais temidos caçadores do mundo.",
            "https://www.imdb.com/title/tt8790086/mediaviewer/rm1284204801/?ref_=tt_ov_i"
        );

        double popularidadeImdbMock = 4138.501;
        double avaliacaoImdbMock = 6.6;
        
        var externalShowInfoServiceMock = new Moq.Mock<IExternalShowInfoService>();

        externalShowInfoServiceMock
            .Setup(method => method.GetPopularidadeImdbAsync(It.IsAny<string>()))
            .ReturnsAsync(popularidadeImdbMock);
        
        externalShowInfoServiceMock
            .Setup(method => method.GetAvaliacaoImdbAsync(It.IsAny<string>()))
            .ReturnsAsync(avaliacaoImdbMock);
        
        var adicionarHandler  = new AdicionarHandler(externalShowInfoServiceMock.Object);
        
        //Act
        var result = await adicionarHandler.Handle(commandMock, CancellationToken.None);

        //Assert
        Assert.IsType<ApiResult<AdicionarResponse>>(result);
        Assert.True(result.Success);
        Assert.Empty(result.Errors!);
        
        Assert.Equal(popularidadeImdbMock, result.Data.PopularidadeImdb);
        Assert.Equal(avaliacaoImdbMock, result.Data.AvaliacaoImdb);
        
        externalShowInfoServiceMock.Verify(
            method => method.GetAvaliacaoImdbAsync(It.IsAny<string>()),
            Times.Once
        );
        
        externalShowInfoServiceMock.Verify(
            method => method.GetPopularidadeImdbAsync(It.IsAny<string>()),
            Times.Once
        );
    }
}