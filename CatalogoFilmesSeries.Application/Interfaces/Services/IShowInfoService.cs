using CatalogoFilmesSeries.Domain.ValueObjects;

namespace CatalogoFilmesSeries.Application.Interfaces.Services;

public interface IShowInfoService
{
    Task<ShowInfoVo> GetFilmeImdbInfoAsync(string titulo, int anoLancamento, CancellationToken cancellationToken);
    Task<ShowInfoVo> GetSerieImdbInfoAsync(string titulo, int anoLancamento, CancellationToken cancellationToken);
}