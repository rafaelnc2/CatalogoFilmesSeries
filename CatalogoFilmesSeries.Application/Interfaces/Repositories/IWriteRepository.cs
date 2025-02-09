namespace CatalogoFilmesSeries.Application.Interfaces.Repositories;

public interface IWriteRepository<T> where T : Entity
{
    public Task AddAsync(T entity);
    public Task<T> UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
}