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

        private readonly ICatSqlRepository _catRepository;

        private readonly ICatHttpRepository _catHttpRepository;

        public CatBusiness(IOptions<ApiConfiguration> apiConfiguration, ICatSqlRepository catRepository, ICatHttpRepository catHttpRepository)
        {
            _apiConfiguration = apiConfiguration;
            _catRepository = catRepository;
            _catHttpRepository = catHttpRepository;
        }
        public List<Cat> GetCats()
        {
           return _catRepository.GetCats();
        }

        public async Task<Tuple<Cat, Cat>> GetCatsForVoteAsync()
        {

            var cats = await _catHttpRepository.GetAllCatWithPicture();

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
