using CutestCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutestCat.Business
{
    public interface ICatBusiness
    {
        void Vote(VoteModel model);
        List<Cat> GetCandidates();
        List<Cat> GetCats();
    }
}
