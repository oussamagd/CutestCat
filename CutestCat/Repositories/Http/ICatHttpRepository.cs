using CutestCat.Models;
using System.Collections.Generic;

namespace CutestCat.Repositories.Http
{
    public interface ICatHttpRepository
    {
        List<Cat> GetAllCandidates();
    }
}
