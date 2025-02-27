namespace CatalogoFilmesSeries.Domain.Interfaces.Repositories;

public interface IReadRepository<T> where T : Entity
{
    public Task<T> GetByIdAsync(Guid id);
    public Task<IEnumerable<T>> GetAllAsync();
}