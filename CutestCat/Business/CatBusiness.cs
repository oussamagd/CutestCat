using CutestCat.Models;
using CutestCat.Repositories.Http;
using CutestCat.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CutestCat.Business
{
    public class CatBusiness : ICatBusiness
    {

        private readonly ICatSqlRepository _catRepository;

        private readonly ICatHttpRepository _catHttpRepository;

        public const int NbrOfCandidate = 2;

        public CatBusiness( ICatSqlRepository catRepository, ICatHttpRepository catHttpRepository)
        {
            _catRepository = catRepository;
            _catHttpRepository = catHttpRepository;
        }
        public List<Cat> GetCats()
        {
           return _catRepository.GetCats();
        }

        public List<Cat> GetCandidates()
        {
            var cats =  _catHttpRepository.GetAllCandidates();

            return GetRandomCandidates(cats, NbrOfCandidate);
        }


        public List<Cat> GetRandomCandidates(List<Cat> cats,int number)
        {
            var indexList = GetRandomIndex(cats.Count, number);

            return indexList.Select(index => cats[index]).ToList();
        }

        public List<int> GetRandomIndex(int maxValue,int number)
        {
            var result = new List<int>();
            var random = new Random();
            var newIndex = 0;
            for (int i = 0; i < number; i++)
            {
                do
                {
                    newIndex = random.Next(0, maxValue);
                } while (result.Contains(newIndex));
                result.Add(newIndex);
            }
            return result;
        }

        public void Vote(VoteModel model)
        {
            _catRepository.Vote(model);
        }
    }
}
