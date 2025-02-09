namespace CatalogoFilmesSeries.Application.Interfaces.Repositories.Series;

public interface ISerieReadRepository : IReadRepository<Serie>
{
    public Task<Filme?> SearchByNameAsync(string searchString);
}