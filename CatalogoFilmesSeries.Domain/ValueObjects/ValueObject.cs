namespace CatalogoFilmesSeries.Domain.ValueObjects;

public abstract class ValueObject
{
    protected List<string> _errors = new();
    
    public bool HasErrors { get => _errors.Any(); }
}