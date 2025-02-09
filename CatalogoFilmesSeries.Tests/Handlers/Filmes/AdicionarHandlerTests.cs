using CatalogoFilmesSeries.Application.Interfaces.IIntegrationEvents;
using CatalogoFilmesSeries.Application.Interfaces.Repositories.Filmes;
using CatalogoFilmesSeries.Application.UseCases.Filmes.Adicionar;

namespace CatalogoFilmesSeries.Unit.Tests.Handlers.Filmes;

public class AdicionarHandlerTests
{
    private readonly Mock<ILogger<AdicionarHandler>> _loggerMock;
    
    private readonly Mock<IExternalShowInfoService> _externalShowInfoServiceMock;
    private readonly Mock<IFilmeWriteRepository> _filmeWriteRepositoryMock;
    private readonly Mock<IFilmeReadRepository> _filmeReadRepositoryMock;

    private readonly Mock<IIntegrationEventPublisher> _integrationEventPublisherMock;
    
    public AdicionarHandlerTests()
    {
        _loggerMock = new Mock<ILogger<AdicionarHandler>>();
        _externalShowInfoServiceMock = new Mock<IExternalShowInfoService>();
        _filmeWriteRepositoryMock = new Mock<IFilmeWriteRepository>();
        _filmeReadRepositoryMock = new Mock<IFilmeReadRepository>();
        _integrationEventPublisherMock = new Mock<IIntegrationEventPublisher>();
    }
    
    [Fact(DisplayName = "Criar um novo filme com sucesso")]
    public async Task AdicionarHandler_Deve_Adicionar_Um_Novo_Filme_Com_Sucesso()
    {
        //Arrange
        ImdbInfoVo imdbInfoMock = new(1, 1, 1);
        
        var commandMock = new AdicionarCommand(
            "Kraven, o Caçador",
            "Kraven the Hunter",
            2024,
            16,
            127,
            "A complexa relação de Kraven com o pai, Nikolai Kravinoff, o leva a uma jornada de vingança com consequências brutais, o motivando a se tornar um dos maiores e mais temidos caçadores do mundo.",
            "https://www.imdb.com/title/tt8790086/mediaviewer/rm1284204801/?ref_=tt_ov_i"
        );

        ImdbDataDto imdbDataMock = new(4138.501, 6.6, 100);

        _externalShowInfoServiceMock
            .Setup(method => method.GetImdbDataAsync(It.IsAny<string>()))
            .ReturnsAsync(imdbDataMock);
        
        var adicionarHandler  = new AdicionarHandler(_loggerMock.Object, 
            _externalShowInfoServiceMock.Object, _filmeWriteRepositoryMock.Object, _filmeReadRepositoryMock.Object,
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
        
        _filmeWriteRepositoryMock.Verify(
            method => method.AddAsync(It.IsAny<Filme>()),
            Times.Once
        );
    }
    
    [Fact(DisplayName = "Deve retornar um erro quando já existir um filme com o mesmo nome")]
    public async Task AdicionarHandler_Deve_Retornar_Um_Erro_Quando_O_Filme_Ja_Existir()
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

        ImdbInfoVo imdbInfoMock = new(4138.501, 6.6, 100);
        
        var filmeMcok = Filme.Create(
            commandMock.Titulo, 
            commandMock.TituloOriginal, 
            commandMock.AnoLancamento,
            commandMock.ClassificacaoEtaria, 
            commandMock.Duracao, 
            commandMock.Sinopse, 
            commandMock.UrlImagem,
            imdbInfoMock
        );

        _filmeReadRepositoryMock
            .Setup(method => method.SearchByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(filmeMcok);
        
        var adicionarHandler  = new AdicionarHandler(_loggerMock.Object, 
            _externalShowInfoServiceMock.Object, _filmeWriteRepositoryMock.Object, _filmeReadRepositoryMock.Object,
            _integrationEventPublisherMock.Object);
        
        //Act
        var result = await adicionarHandler.Handle(commandMock, CancellationToken.None);

        //Assert
        Assert.IsType<ApiResult<AdicionarResponse>>(result);
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal($"Titulo informado já existe com ID {filmeMcok.Id}", result.Message);

        _filmeReadRepositoryMock.Verify(
            method => method.SearchByNameAsync(It.IsAny<string>()),
            Times.Once
        );
        
        _externalShowInfoServiceMock.Verify(
            method => method.GetImdbDataAsync(It.IsAny<string>()),
            Times.Never
        );
        
        _filmeWriteRepositoryMock.Verify(
            method => method.AddAsync(It.IsAny<Filme>()),
            Times.Never
        );
    }
    
    [Fact(DisplayName = "Deve retornar um erro quando forem informados dados inválidos para a criação de um filme")]
    public async Task AdicionarHandler_Deve_Retornar_Um_Erro_Quando_Os_Dados_Informados_Sao_Invalidos()
    {
        //Arrange
        var commandMock = new AdicionarCommand(
            "",
            "",
            -1,
            -1,
            0,
            "A",
            "www"
        );

        Filme filmeMock = null;
        
        _filmeReadRepositoryMock
            .Setup(method => method.SearchByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(filmeMock);
        
        var adicionarHandler  = new AdicionarHandler(_loggerMock.Object, 
            _externalShowInfoServiceMock.Object, _filmeWriteRepositoryMock.Object, _filmeReadRepositoryMock.Object,
            _integrationEventPublisherMock.Object);
        
        //Act
        var result = await adicionarHandler.Handle(commandMock, CancellationToken.None);

        //Assert
        Assert.IsType<ApiResult<AdicionarResponse>>(result);
        Assert.False(result.Success);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(7, result.Errors.Count());

        _filmeReadRepositoryMock.Verify(
            method => method.SearchByNameAsync(It.IsAny<string>()),
            Times.Once
        );

        _externalShowInfoServiceMock.Verify(
            method => method.GetImdbDataAsync(It.IsAny<string>()),
            Times.Once
        );
        
        _filmeWriteRepositoryMock.Verify(
            method => method.AddAsync(It.IsAny<Filme>()),
            Times.Never
        );
    }
}