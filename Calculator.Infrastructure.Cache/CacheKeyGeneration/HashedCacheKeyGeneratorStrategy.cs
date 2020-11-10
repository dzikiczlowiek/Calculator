using System;
using System.Collections;
using System.Linq;
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
                var tmp = new object[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] is IEnumerable)
                    {
                        tmp[i] = MapEnumerable(args[i]);
                    }
                    else
                    {
                        tmp[i] = args[i];
                    }
                }
                var serializer = new CerasSerializer();
                var bytes = serializer.Serialize(tmp);

                var hashRaw = md5.ComputeHash(bytes);
                var result = BitConverter.ToString(hashRaw)
                   .Replace("-", string.Empty)
                   .ToLower();
                return result;

                object MapEnumerable(object enumerable)
                {
                    var generics = enumerable.GetType().GetGenericArguments();
                    if (generics.Any())
                    {
                        var generic = generics[0];
                        var meth = typeof(System.Linq.Enumerable).GetMethod("ToArray");
                        var genericMeth = meth.MakeGenericMethod(generic);

                        var res = genericMeth.Invoke(null, new object[] { enumerable });
                        return res;
                    }

                    return enumerable;
                }
            }
        }

        public bool IsApplicable(IInvocation invocation)
        {
            return true;
        }
    }
}
