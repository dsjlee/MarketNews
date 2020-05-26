using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WasmServer.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WasmServer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly ILogger<NewsController> logger;
        private readonly IHttpClientFactory clientFactory;

        public NewsController(ILogger<NewsController> logger, IMemoryCache cache, IHttpClientFactory clientFactory)
        {
            this.logger = logger;
            this.cache = cache;
            this.clientFactory = clientFactory;
        }      

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            // Look for cache key.
            if (!cache.TryGetValue(CacheKey.MarketNews, out Article[] cacheEntry) && !Helper.IsNewCaching)
            {
                // Key not in cache, so get data.
                Helper.IsNewCaching = true;
                try
                {
                    var client = clientFactory.CreateClient(HttpClientName.FinnHub); // Startup.cs -> ConfigureServices
                    HttpResponseMessage response = await client.GetAsync($"api/v1/news?category=general&token={Helper.FinnHubApiKey}");

                    if (response.IsSuccessStatusCode)
                    {
                        cacheEntry = await response.Content.ReadFromJsonAsync<Article[]>();
                        CacheInMemory(CacheKey.MarketNews, cacheEntry, 10);
                    }
                }
                catch(Exception ex)
                {
                    //logger.LogError();
                }
                finally
                {
                    Helper.IsNewCaching = false;
                }             
            }

            return Json(cacheEntry);
        }

        [NonAction]
        private void CacheInMemory(string cacheKey, Article[] cacheEntry, int expireMinute)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(expireMinute));
            //.RegisterPostEvictionCallback(callback: EvictionCallback, state: this);
            cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            //System.Diagnostics.Debug.WriteLine($"Cached {cacheKey}: {DateTime.Now}");
        }

        [NonAction]
        private static void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            System.Diagnostics.Debug.WriteLine("EvictionCallback");
        }
    }
}
