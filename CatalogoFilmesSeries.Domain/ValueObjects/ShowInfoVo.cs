namespace CatalogoFilmesSeries.Domain.ValueObjects;

public sealed class ShowInfoVo : ValueObject
{
    public ShowInfoVo(double? popularity, double? voteAverage, int? voteCount)
    {
        Validate(ref popularity, ref voteAverage, ref voteCount);
        
        Popularity = popularity.Value;
        VoteAverage = voteAverage.Value;
        VoteCount = voteCount.Value;
    }

    public double Popularity { get; private set; }
    public double VoteAverage { get; private set; }
    public int VoteCount { get; private set; }

    private void Validate(ref double? popularity, ref double? voteAverage, ref int? voteCount)
    {
        if (popularity.HasValue is false || popularity < 0)
            popularity = 0;
        
        if (voteAverage.HasValue is false || voteAverage < 0)
            voteAverage = 0;

        if (voteCount.HasValue is false || voteCount < 0)
            voteCount = 0;
    }
}