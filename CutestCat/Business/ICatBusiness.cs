using CutestCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutestCat.Business
{
    public interface ICatBusiness
    {
        Task SendVote(VoteResultModel model);
        Task<Tuple<Cat, Cat>> GetCatsForVoteAsync();
        List<Cat> GetCats();
    }
}
