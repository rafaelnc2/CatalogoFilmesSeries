namespace CatalogoFilmesSeries.Adapters.Outbound;

public sealed class ShowInfoTMDBAdapter : IShowInfoService
{
    public async Task<ShowInfoVo> GetFilmeImdbInfoAsync(string titulo, int anoLancamento, CancellationToken cancellationToken)
    {
        //Acessar API externa tmdb

        ExternalShowInfoDto externalShowInfoDto = new ExternalShowInfoDto(10, 10, 10);
        
        return new(externalShowInfoDto?.Popularity, externalShowInfoDto?.VoteAverage, externalShowInfoDto?.VoteCount);
    }

    public async Task<ShowInfoVo> GetSerieImdbInfoAsync(string titulo, int anoLancamento, CancellationToken cancellationToken)
    {
        //Acessar API externa tmdb

        ExternalShowInfoDto externalShowInfoDto = new ExternalShowInfoDto(10, 10, 10);
        
        return new(externalShowInfoDto?.Popularity, externalShowInfoDto?.VoteAverage, externalShowInfoDto?.VoteCount);
    }
}