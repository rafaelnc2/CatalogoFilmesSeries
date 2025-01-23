namespace CatalogoFilmesSeries.Application.Services;

public interface IExternalShowInfoService
{
    public Task<double> GetAvaliacaoImdbAsync(string tituloOriginal);
    
    public Task<double> GetPopularidadeImdbAsync(string tituloOriginal);
}