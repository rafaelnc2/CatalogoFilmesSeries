using CatalogoFilmesSeries.Domain.Entities;
using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Series;

namespace CatalogoFilmesSeries.Adapters.Outbound.Repositories.Series;

public class SerieWriteRepository : ISerieWriteRepository
{
    public Task AddAsync(Serie entity)
    {
        throw new NotImplementedException();
    }

    public Task<Serie> UpdateAsync(Serie entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Serie entity)
    {
        throw new NotImplementedException();
    }
}