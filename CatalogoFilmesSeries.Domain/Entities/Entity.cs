using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoFilmesSeries.Domain.Entities;

public abstract class Entity
{
    protected List<string> _errors = new();
    
    public Guid Id { get; protected set; }
    public DateTime DataInclusao { get; protected  set; }
    public DateTime? DataAtualizacao { get; protected  set; }

    [NotMapped]
    public IReadOnlyList<string> Errors { get => _errors; }
    [NotMapped]
    public bool HasErrors { get => _errors.Any(); }
}