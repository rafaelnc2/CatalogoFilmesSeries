using CatalogoFilmesSeries.Domain.Interfaces.Repositories.Filmes;

namespace CatalogoFilmesSeries.Adapters.Outbound.Repositories.Filmes;

public sealed class FilmeWriteRepository : IFilmeWriteRepository
{
    public Task AddAsync(Filme entity)
    {
        throw new NotImplementedException();
    }

    public Task<Filme> UpdateAsync(Filme entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Filme entity)
    {
        throw new NotImplementedException();
    }
}