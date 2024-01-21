using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GoogleNews.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {

        private readonly IMemoryCache _cache;
        private const string CacheKey = "GoogleNewsFeed";

        public ManageController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }


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
