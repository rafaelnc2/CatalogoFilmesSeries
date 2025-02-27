using CatalogoFilmesSeries.Domain.Interfaces.Repositories;

namespace CatalogoFilmesSeries.Adapters.Outbound.Repositories;

public abstract class WriteRepository<T> : IWriteRepository<T> where T : Entity
{
    public Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }
}