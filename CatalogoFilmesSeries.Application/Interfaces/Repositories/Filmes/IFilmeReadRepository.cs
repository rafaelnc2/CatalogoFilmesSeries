﻿namespace CatalogoFilmesSeries.Application.Interfaces.Repositories.Filmes;

public interface IFilmeReadRepository : IReadRepository<Filme>
{
    public Task<Filme?> SearchByNameAsync(string searchString);
}