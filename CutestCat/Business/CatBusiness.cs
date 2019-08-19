using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutestCat.Models;

namespace CutestCat.Business
{
    public class CatBusiness : ICatBusiness
    {
        public CatBusiness()
        {

        }
        public Task<List<Cat>> GetCats()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<Cat, Cat>> GetCatsForVote()
        {
            throw new NotImplementedException();
        }

        public Task SendVote(VoteResultModel model)
        {
            throw new NotImplementedException();
        }
    }
}
