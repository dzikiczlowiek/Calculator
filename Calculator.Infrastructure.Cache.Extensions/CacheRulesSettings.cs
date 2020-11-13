using System;
using System.Collections.Generic;

namespace Calculator.Infrastructure.Cache.Extensions
{
    public class CacheRulesSettings
    {
        public List<CacheRule> Rules { get; set; }
        public int? DefaultCacheExpirationTimeInSeconds { get; set; }

        [Serializable]
        public class CacheRule
        {
            public string Type { get; set; }
            public List<MethodDetails> Methods { get; set; }
        }

        [Serializable]
        public class MethodDetails
        {
            public string Name { get; set; }

            public int? ExpirationTimeInSeconds { get; set; }
        }
    }
}
