using DAL_;
using DAL_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GoogleNews.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoogleNewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "GoogleNewsFeed";
        private readonly DAL_.NewsService _newsService;

        public GoogleNewsController(DAL_.NewsService newsService, IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            _newsService = newsService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetNews()
        {
            try
            {
                var news = await _newsService.GetNewsAsync();
                return Ok(news);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetNewsItem(string id)
        {
            try
            {
                // Fetch specific news item based on id
                var newsItem = await _newsService.GetNewsByIdAsync(id);

                if (newsItem != null)
                {
                    return Ok(newsItem);
                }
                else
                {
                    return NotFound("News not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
