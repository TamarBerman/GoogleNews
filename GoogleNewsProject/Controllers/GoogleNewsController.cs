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
        // Dependency Injection - inject the NewsService from the DAL layer
        private readonly DAL_.NewsService _newsService;

        // Ctor - DI
        public GoogleNewsController(DAL_.NewsService newsService )
        {
            _newsService = newsService;
        }


        /// <summary>
        /// Retrieves the news from the RSS external API. 
        /// returns Task<IActionResult> - for async data
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves one specific new-item from the RSS external API ( or from HttpCache). 
        /// according to the new-item id (guid)
        /// returns Task<IActionResult> - for async data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
