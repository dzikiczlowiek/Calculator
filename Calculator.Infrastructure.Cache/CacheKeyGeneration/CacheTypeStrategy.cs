using System;

namespace Calculator.Infrastructure.Cache.CacheKeyGeneration
{
    public abstract class CacheTypeStrategy : ICacheKeyTypeStrategy
    {
        public abstract string GetKey(object instance);

        public abstract bool IsApplicable(Type type);
    }

    public abstract class CacheTypeStrategy<T> : ICacheKeyTypeStrategy<T>
    where T : class
    {
        public abstract string GetKey(T instance);

        public string GetKey(object instance)
        {
            return GetKey((T)instance);
        }

        public bool IsApplicable(Type type)
        {
            return type == typeof(T);
        }
    }
}
