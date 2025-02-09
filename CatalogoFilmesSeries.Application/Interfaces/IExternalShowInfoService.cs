using CatalogoFilmesSeries.Application.Dtos;

namespace CatalogoFilmesSeries.Application.Interfaces;

public interface IExternalShowInfoService
{
    public Task<ImdbDataDto> GetImdbDataAsync(string tituloOriginal);
}