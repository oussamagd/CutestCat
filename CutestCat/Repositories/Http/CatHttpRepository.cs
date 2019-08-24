using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutestCat.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CutestCat.Repositories.Http
{
    public class CatHttpRepository : ICatHttpRepository
    {
        private readonly IOptions<ApiConfiguration> _apiConfiguration;
        private readonly IMemoryCache _cache;
        public CatHttpRepository(IOptions<ApiConfiguration> apiConfiguration, IMemoryCache memoryCache)
        {
            _apiConfiguration = apiConfiguration;
            _cache = memoryCache;

        }
        public List<Cat> GetAllCandidates()
        {
            List<Cat> allCandidates;
            if (!_cache.TryGetValue("AllCatCandidates", out allCandidates))
            {
                var catsDto = HttpHelper.Get<CatsHttpObject>(_apiConfiguration.Value.CatApiPath);
                allCandidates = catsDto.Images.Select(cat => cat.ToModel()).ToList();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
                _cache.Set("AllCatCandidates", allCandidates, cacheEntryOptions);
            }

            return allCandidates;
        }
    }
}
