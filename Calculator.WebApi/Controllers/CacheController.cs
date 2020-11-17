using System.Linq;
using System.Threading.Tasks;

using Calculator.Infrastructure.Cache;

using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheProvider cacheProvider;

        public CacheController(ICacheProvider cacheProvider)
        {
            this.cacheProvider = cacheProvider;
        }

        [HttpGet(nameof(ShowCache))]
        public async Task<IActionResult> ShowCache()
        {
            var cacheEntries = cacheProvider.ActiveKeys()
                .Select(x=> new { 
                    x.Key, 
                    x.AbsoluteExpiration, 
                    AbsoluteExpirationRelativeToNow = x.AbsoluteExpirationRealtiveToNow?.ToString(), 
                    x.ValueType });
            return Ok(cacheEntries);
        }

        [HttpGet(nameof(ClearCache))]
        public async Task<IActionResult> ClearCache()
        {
            cacheProvider.ClearCache();
            return Ok();
        }
    }
}
