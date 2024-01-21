using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GoogleNews.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {

        // HttpCache | Dependecy Injection of IMemoryCache .Net Core Service
        private readonly IMemoryCache _cache;
        private const string CacheKey = "GoogleNewsFeed";

        // Ctor - DI IMemoryCache
        public ManageController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }


        /// <summary>
        /// Option for the manager to clear the cache not automatically
        /// using the Remove() function of IMemoryCache Service
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult ClearCache()
        {
            try
            {
                _cache.Remove(CacheKey);
                Console.WriteLine("Cleared cache");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error clearing cache: " + ex.Message);
                return BadRequest();
            }
        }
    }
}
