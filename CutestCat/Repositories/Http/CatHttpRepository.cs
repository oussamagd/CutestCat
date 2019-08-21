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
        public List<Cat> GetAllCandidates()
        {
            var catsDto = HttpHelper.Get<CatsHttpObject>(_apiConfiguration.Value.CatApiPath);
            return catsDto.Images.Select(cat => cat.ToModel()).ToList();
        }
    }
}
