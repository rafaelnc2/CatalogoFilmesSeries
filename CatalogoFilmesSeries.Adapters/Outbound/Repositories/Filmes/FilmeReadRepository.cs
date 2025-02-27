using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Filmes;

namespace CatalogoFilmesSeries.Adapters.Outbound.Repositories.Filmes;

public sealed class FilmeReadRepository : IFilmeReadRepository
{
    private readonly ApplicationDbContext _ctx;

    public FilmeReadRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }


    public Task<Filme> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Filme>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Filme?> SearchByNameAsync(string searchString)
    {
        throw new NotImplementedException();
    }
}