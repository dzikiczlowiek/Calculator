using System;
using System.Security.Cryptography;

using Castle.DynamicProxy;

using Ceras;

namespace Calculator.Infrastructure.Cache.CacheKeyGeneration
{
    public sealed class HashedCacheKeyGeneratorStrategy : ICacheKeyGeneratorStrategy
    {
        public string GenerateKey(IInvocation invocation)
        {
            using(var md5 = MD5.Create())
            {
                var args = invocation.Arguments;
                var serializer = new CerasSerializer();
                var bytes = serializer.Serialize(args);

                var hashRaw = md5.ComputeHash(bytes);
                var result = BitConverter.ToString(hashRaw)
                   .Replace("-", string.Empty)
                   .ToLower();
                return result;
            }
        }

        public bool IsApplicable(IInvocation invocation)
        {
            return true;
        }
    }
}
