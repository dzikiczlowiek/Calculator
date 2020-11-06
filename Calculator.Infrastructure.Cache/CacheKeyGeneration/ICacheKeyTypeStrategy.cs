using System;

using Castle.DynamicProxy.Contributors;
namespace Calculator.Infrastructure.Cache.CacheKeyGeneration
{
    public interface ICacheKeyTypeStrategy
    {
        bool IsApplicable(Type type);

        string GetKey(object instance);
    }

    public interface ICacheKeyTypeStrategy<T> : ICacheKeyTypeStrategy
        where T : class
    {
        string GetKey(T instance);
    }
}
