using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutestCat.Models;
using CutestCat.Repositories.Http;
using CutestCat.Repositories.Sql;
using Microsoft.Extensions.Options;

namespace CutestCat.Business
{
    public class CatBusiness : ICatBusiness
    {
        private readonly IOptions<ApiConfiguration> _apiConfiguration;

        private readonly ICatRepository _catRepository;

       public CatBusiness(IOptions<ApiConfiguration> apiConfiguration, ICatRepository catRepository)
        {
            _apiConfiguration = apiConfiguration;
            _catRepository = catRepository;
        }
        public List<Cat> GetCats()
        {
           return _catRepository.GetCats();
        }

        public async Task<Tuple<Cat, Cat>> GetCatsForVoteAsync()
        {
            var cats = await HttpHelper.Get<List<Cat>>(_apiConfiguration.Value.CatApiPath);
            return GetTwoRandomCats(cats);
        }

        public Tuple<Cat, Cat> GetTwoRandomCats(List<Cat> cats)
        {
            var random = new Random();
            int firstCatIndex = random.Next(0, cats.Count);
            int secondCatIndex;
            do
            {
                secondCatIndex = random.Next(0, cats.Count);
            } while (secondCatIndex == firstCatIndex);

            return new Tuple<Cat, Cat>(cats[firstCatIndex], cats[secondCatIndex]);
        }

        public Task SendVote(VoteResultModel model)
        {
            throw new NotImplementedException();
        }
    }
}
