using GoogleNewsProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;

namespace GoogleNewsProject.Services
{
    public class NewsService
    {
        // API link
        private const string RssFeedUrl = "http://news.google.com/news?pz=1&cf=all&ned=en_il&hl=en&output=rss";

        private const string CacheKey = "GoogleNewsFeed";

        private readonly IMemoryCache _cache;



        //Ctor with DI of IMemoryCache interface
        public NewsService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<IEnumerable<NewsItem>?> GetNewsAsync()
        {
            // several users accessing our service which means there could be multiple requests accessing our in-memory cache. One way to make sure that no two different users get different results is by utilising .Net locks
            // https://sahansera.dev/in-memory-caching-aspcore-dotnet/
            var semaphore = new SemaphoreSlim(1, 1);

            // Try to get news from cache
            if (_cache.TryGetValue(CacheKey, out IEnumerable<NewsItem> cachedNews))
            {
                Console.WriteLine("News found in cache");
                return cachedNews;
            }
            // If news doesn't exists in cache
            try
            {

                await semaphore.WaitAsync();

                // Recheck to make sure it didn't populate before entering semaphore
                if (_cache.TryGetValue(CacheKey, out cachedNews))
                {
                    Console.WriteLine("News found in cache");
                    return cachedNews;
                }

                Console.WriteLine("products NOT found in cache. loading them...");
                // Fetch news
                cachedNews = await this.fetchNews();
                return cachedNews;
            }
            catch (Exception ex)
            {
                // Handle exception 
                Console.WriteLine($"Error fetching news: {ex.Message}");
                return null;
            }
            finally
            {
                // That it won't be locked forever
                semaphore.Release();
            }
        }

        /// <summary>
        ///  Call the external API, 
        ///  gets the data ad an XML file
        ///  convert the XML deta to a list of NewsItem
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<NewsItem>> fetchNews()
        {
            using (var httpClient = new HttpClient())
            {
                var rssData = await httpClient.GetStringAsync(RssFeedUrl);

                var xmlDoc = XDocument.Parse(rssData);

                var newsList = new List<NewsItem>();
                foreach (var item in xmlDoc.Descendants("item"))
                {
                    var newsItem = new NewsItem
                    {
                        Title = item.Element("title")?.Value,
                        Body = item.Element("description")?.Value,
                        Link = item.Element("link")?.Value
                    };

                    newsList.Add(newsItem);
                }

                // Cache the news

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(CacheKey, newsList, cacheEntryOptions);

                Console.WriteLine("News loaded from API and cached");

                return newsList;

            }

        }
    }
}
