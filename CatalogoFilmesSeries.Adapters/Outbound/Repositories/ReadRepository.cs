﻿using CatalogoFilmesSeries.Domain.Entities;
using CatalogoFilmesSeries.Domain.Interfaces.Repositories;

namespace CatalogoFilmesSeries.Adapters.Outbound.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : Entity
{
    public Task<T> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}