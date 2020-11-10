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

        public void OnGet()
        {

        }
    }
}
