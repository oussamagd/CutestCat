using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutestCat.Models;
using Microsoft.Extensions.Options;

namespace CutestCat.Repositories.Http
{
    public class CatHttpRepository : ICatHttpRepository
    {
        private readonly IOptions<ApiConfiguration> _apiConfiguration;
        public CatHttpRepository(IOptions<ApiConfiguration> apiConfiguration)
        {
            _apiConfiguration = apiConfiguration;
        }
        public async Task<List<Cat>> GetAllCatWithPicture()
        {
            var cats = (await HttpHelper.Get<CatImagesHttpObject>(_apiConfiguration.Value.CatApiPath)).Images;
            return cats.Select(cat => cat.ToModel()).ToList();
        }
    }
}
