using CutestCat.Models;
using System.Collections.Generic;

namespace CutestCat.Repositories.Sql
{
    public interface ICatSqlRepository
    {
        List<Cat> GetCats();
        void Vote(VoteModel model);
    }
}
