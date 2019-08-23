using CutestCat.Business;
using CutestCat.Models;
using CutestCatTest.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CutestCatTest.Controller
{
    public class CatBusinessMock : ICatBusiness
    {
        public List<Cat> GetCandidates()
        {
           return CatFactory.GetAllCandidates().Take(CatBusiness.NbrOfCandidate).ToList();
        }

        public List<Cat> GetCats()
        {
            return CatFactory.GetCats();
        }

        public void Vote(VoteModel model)
        {
           
        }
    }
}
