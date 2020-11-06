using System;

namespace Calculator.Infrastructure.Cache
{
    public class CacheManagerFacility
    {
        private static readonly object SyncObject = new object();
        private readonly ICacheManager cacheManager;

        public CacheManagerFacility(ICacheManager cacheManager)
        {
            if (Current !=null)
            {
                throw new InvalidOperationException($"Only one instance of '{nameof(cacheManager)}' allowed.");
            }

            this.cacheManager = cacheManager;
        }

        public static ICacheManager Current { get; private set; }

        public void Start()
        {
            lock (SyncObject)
            {
                Current = cacheManager;
            }
        }

        public void Stop()
        {
            lock (SyncObject)
            {
                Current = null;
            }
        }
    }
}
