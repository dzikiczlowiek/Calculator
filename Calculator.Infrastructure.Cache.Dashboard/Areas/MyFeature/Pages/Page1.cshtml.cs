using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calculator.Infrastructure.Cache.Dashboard.MyFeature.Pages
{
    public class Page1Model : PageModel
    {
        private readonly ICacheProvider cacheProvider;

        public Page1Model(ICacheProvider cacheProvider)
        {
            this.cacheProvider = cacheProvider;
        }

        public ViewModel Data { get; private set; }

        public void OnGet()
        {
            Data = new ViewModel();
            Data.CacheEntries = cacheProvider.ActiveKeys().ToList();
        }

        public class ViewModel
        {
            public List<string> CacheEntries { get; set; } = new List<string>();
        }
    }
}
