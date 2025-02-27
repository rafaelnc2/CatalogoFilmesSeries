using CatalogoFilmesSeries.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilmesSeries.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Serie> Series { get; set; }
}