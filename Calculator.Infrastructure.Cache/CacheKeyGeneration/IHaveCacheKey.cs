namespace Calculator.Infrastructure.Cache.CacheKeyGeneration
{
    public interface IHaveCacheKey
    {
        string CacheKey { get; }
    }
}
