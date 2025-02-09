using CatalogoFilmesSeries.Application.Interfaces.IIntegrationEvents;
using CatalogoFilmesSeries.Application.Interfaces.Repositories.Series;
using CatalogoFilmesSeries.Application.UseCases.Series.Adicionar;

namespace CatalogoFilmesSeries.Unit.Tests.Handlers.Series;

public class AdicionarHandlerTests
{
    private readonly Mock<ILogger<AdicionarHandler>> _loggerMock;
    
    private readonly Mock<IExternalShowInfoService> _externalShowInfoServiceMock;
    private readonly Mock<ISerieWriteRepository> _serieWriteRepositoryMock;
    private readonly Mock<ISerieReadRepository> _serieReadRepositoryMock;

    private readonly Mock<IIntegrationEventPublisher> _integrationEventPublisherMock;
    
    public AdicionarHandlerTests()
    {
        _loggerMock = new Mock<ILogger<AdicionarHandler>>();
        _externalShowInfoServiceMock = new Mock<IExternalShowInfoService>();
        _serieWriteRepositoryMock = new Mock<ISerieWriteRepository>();
        _serieReadRepositoryMock = new Mock<ISerieReadRepository>();
        _integrationEventPublisherMock = new Mock<IIntegrationEventPublisher>();
    }
    
    [Fact(DisplayName = "Criar uma nova série com sucesso")]
    public async Task AdicionarHandler_Deve_Adicionar_Uma_Nova_Serie_Com_Sucesso()
    {
        //Arrange
        ImdbInfoVo imdbInfoMock = new(1, 1, 1);
        
        var commandMock = new AdicionarCommand(
            "Reacher",
            "Reacher",
            2022,
            16,
            "Quando o policial militar aposentado Jack Reacher é preso por um assassinato que não cometeu, ele se vê no meio de uma trama mortal cheia de policiais corruptos, empresários obscuros e políticos conspiradores. Só com sua inteligência, ele precisa descobrir o que está havendo em Margrave, Geórgia",
            "https://www.imdb.com/title/tt8790086/mediaviewer/rm1284204801/?ref_=tt_ov_i",
            3,
            8,
            45
        );

        ImdbDataDto imdbDataMock = new(4138.501, 6.6, 100);

        _externalShowInfoServiceMock
            .Setup(method => method.GetImdbDataAsync(It.IsAny<string>()))
            .ReturnsAsync(imdbDataMock);
        
        var adicionarHandler  = new AdicionarHandler(_loggerMock.Object, 
            _externalShowInfoServiceMock.Object, _serieWriteRepositoryMock.Object, _serieReadRepositoryMock.Object,
            _integrationEventPublisherMock.Object);
        
        //Act
        var result = await adicionarHandler.Handle(commandMock, CancellationToken.None);

        //Assert
        Assert.IsType<ApiResult<AdicionarResponse>>(result);
        Assert.True(result.Success);
        Assert.Empty(result.Errors!);
        
        Assert.Equal(imdbDataMock.Popularity, result.Data.PopularidadeImdb);
        Assert.Equal(imdbDataMock.VoteAverage, result.Data.AvaliacaoImdb);
        Assert.Equal(imdbDataMock.VoteCount, result.Data.QuantidadeVotosImdb);
        
        _externalShowInfoServiceMock.Verify(
            method => method.GetImdbDataAsync(It.IsAny<string>()),
            Times.Once
        );
        
        _serieWriteRepositoryMock.Verify(
            method => method.AddAsync(It.IsAny<Serie>()),
            Times.Once
        );
    }
}