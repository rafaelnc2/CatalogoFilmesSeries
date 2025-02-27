namespace CatalogoFilmesSeries.Domain.Interfaces.Repositories.Series;

public interface ISerieReadRepository : IReadRepository<Serie>
{
    public Task<Serie?> SearchByNameAsync(string searchString);
}