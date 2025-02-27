using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Series;

namespace CatalogoFilmesSeries.Adapters.Outbound.Repositories.Series;

public class SerieReadRepository : ISerieReadRepository
{
    public Task<Serie> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Serie>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Serie?> SearchByNameAsync(string searchString)
    {
        throw new NotImplementedException();
    }
}