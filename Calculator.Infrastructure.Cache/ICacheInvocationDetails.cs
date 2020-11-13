using System;

namespace Calculator.Infrastructure.Cache
{
    public sealed class CacheInvocationDetails
    {
        private CacheInvocationDetails(TimeSpan expirationTime)
        {
            ExpirationTime = expirationTime;
            IsCacheable = true;
        }

        private CacheInvocationDetails() => IsCacheable = false;

        public static CacheInvocationDetails NotCacheable => new CacheInvocationDetails();

        public static CacheInvocationDetails CacheFor(TimeSpan expirationTime) => new CacheInvocationDetails(expirationTime);

        public bool IsCacheable { get; }

        public TimeSpan? ExpirationTime { get; }
    }
}
