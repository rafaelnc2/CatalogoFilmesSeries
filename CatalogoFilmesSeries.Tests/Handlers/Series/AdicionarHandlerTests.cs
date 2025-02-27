using CatalogoFilmesSeries.Application.Interfaces.IIntegrationEvents;
using CatalogoFilmesSeries.Application.Interfaces.Services;
using CatalogoFilmesSeries.Application.UseCases.Series.Adicionar;
using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Series;

namespace CatalogoFilmesSeries.Unit.Tests.Handlers.Series;

public class AdicionarHandlerTests
{
    private readonly Mock<ILogger<AdicionarHandler>> _loggerMock;
    
    private readonly Mock<IShowInfoService> _imdbInfoServiceMock;
    private readonly Mock<ISerieWriteRepository> _serieWriteRepositoryMock;
    private readonly Mock<ISerieReadRepository> _serieReadRepositoryMock;

    private readonly Mock<IIntegrationEventPublisher> _integrationEventPublisherMock;
    
    public AdicionarHandlerTests()
    {
        _loggerMock = new Mock<ILogger<AdicionarHandler>>();
        _imdbInfoServiceMock = new Mock<IShowInfoService>();
        _serieWriteRepositoryMock = new Mock<ISerieWriteRepository>();
        _serieReadRepositoryMock = new Mock<ISerieReadRepository>();
        _integrationEventPublisherMock = new Mock<IIntegrationEventPublisher>();
    }
    
    [Fact(DisplayName = "Criar uma nova série com sucesso")]
    public async Task AdicionarHandler_Deve_Adicionar_Uma_Nova_Serie_Com_Sucesso()
    {
        //Arrange
        ShowInfoVo showInfoMock = new(1, 1, 1);
        
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

        ShowInfoVo showDataMock = new(4138.501, 6.6, 100);

        _imdbInfoServiceMock
            .Setup(method => method.GetSerieImdbInfoAsync(It.IsAny<string>(),It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(showDataMock);
        
        var adicionarHandler  = new AdicionarHandler(
            _loggerMock.Object, 
            _imdbInfoServiceMock.Object, 
            _serieWriteRepositoryMock.Object, 
            _serieReadRepositoryMock.Object,
            _integrationEventPublisherMock.Object
        );
        
        //Act
        var result = await adicionarHandler.Handle(commandMock, CancellationToken.None);

        //Assert
        Assert.IsType<ApiResult<AdicionarResponse>>(result);
        Assert.True(result.Success);
        Assert.Empty(result.Errors!);
        
        Assert.Equal(showDataMock.Popularity, result.Data.PopularidadeImdb);
        Assert.Equal(showDataMock.VoteAverage, result.Data.AvaliacaoImdb);
        Assert.Equal(showDataMock.VoteCount, result.Data.QuantidadeVotosImdb);
        
        _imdbInfoServiceMock.Verify(
            method => method.GetSerieImdbInfoAsync(It.IsAny<string>(),It.IsAny<int>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
        
        _serieWriteRepositoryMock.Verify(
            method => method.AddAsync(It.IsAny<Serie>()),
            Times.Once
        );
    }
    
    [Fact(DisplayName = "Deve retornar um erro quando já existir uma série com o mesmo nome")]
    public async Task AdicionarHandler_Deve_Retornar_Um_Erro_Quando_A_Serie_Ja_Existir()
    {
        //Arrange
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

        ShowInfoVo showInfoMock = new(4138.501, 6.6, 100);
        
        var serieMock = Serie.Create(
            commandMock.Titulo, 
            commandMock.TituloOriginal, 
            commandMock.AnoLancamento, 
            commandMock.ClassificacaoEtaria, 
            commandMock.Sinopse, 
            commandMock.UrlImagem, 
            commandMock.Termporadas, 
            commandMock.QuantidadeEpisodios, 
            commandMock.DuracaoEpisodios, 
            showInfoMock
        );

        _serieReadRepositoryMock
            .Setup(method => method.SearchByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(serieMock);

        _imdbInfoServiceMock
            .Setup(method => method.GetSerieImdbInfoAsync(It.IsAny<string>(),It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(showInfoMock);
        
        var adicionarHandler  = new AdicionarHandler(
            _loggerMock.Object, 
            _imdbInfoServiceMock.Object, 
            _serieWriteRepositoryMock.Object, 
            _serieReadRepositoryMock.Object,
            _integrationEventPublisherMock.Object
        );
        
        //Act
        var result = await adicionarHandler.Handle(commandMock, CancellationToken.None);

        //Assert
        Assert.IsType<ApiResult<AdicionarResponse>>(result);
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal($"Titulo informado já existe com ID {serieMock.Id}", result.Message);

        _serieReadRepositoryMock.Verify(
            method => method.SearchByNameAsync(It.IsAny<string>()),
            Times.Once
        );
        
        _imdbInfoServiceMock.Verify(
            method => method.GetSerieImdbInfoAsync(It.IsAny<string>(),It.IsAny<int>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
        
        _serieWriteRepositoryMock.Verify(
            method => method.AddAsync(It.IsAny<Serie>()),
            Times.Never
        );
    }
    
    [Fact(DisplayName = "Deve retornar um erro quando forem informados dados inválidos para a criação de uma série")]
    public async Task AdicionarHandler_Deve_Retornar_Um_Erro_Quando_Os_Dados_Informados_Sao_Invalidos()
    {
        //Arrange
        var commandMock = new AdicionarCommand(
            "",
            "",
            -1,
            -1,
            "",
            "www.",
            -1,
            -1,
            -1
        );

        Serie serieMock = null;
        
        _serieReadRepositoryMock
            .Setup(method => method.SearchByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(serieMock);
        
        var adicionarHandler  = new AdicionarHandler(
            _loggerMock.Object, 
            _imdbInfoServiceMock.Object, 
            _serieWriteRepositoryMock.Object, 
            _serieReadRepositoryMock.Object,
            _integrationEventPublisherMock.Object
        );
        
        //Act
        var result = await adicionarHandler.Handle(commandMock, CancellationToken.None);

        //Assert
        Assert.IsType<ApiResult<AdicionarResponse>>(result);
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(9, result.Errors.Count());

        _serieReadRepositoryMock.Verify(
            method => method.SearchByNameAsync(It.IsAny<string>()),
            Times.Once
        );

        _imdbInfoServiceMock.Verify(
            method => method.GetSerieImdbInfoAsync(It.IsAny<string>(),It.IsAny<int>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
        
        _serieWriteRepositoryMock.Verify(
            method => method.AddAsync(It.IsAny<Serie>()),
            Times.Never
        );
    }
}