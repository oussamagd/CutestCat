using CutestCat.Models;
using System.Collections.Generic;

namespace CutestCat.Business
{
    public interface ICatBusiness
    {
        void Vote(VoteModel model);
        List<Cat> GetCandidates();
        List<Cat> GetCats();
    }
}
