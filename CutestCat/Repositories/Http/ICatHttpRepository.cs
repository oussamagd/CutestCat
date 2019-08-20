using CutestCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutestCat.Repositories.Http
{
    public interface ICatHttpRepository
    {
        Task<List<Cat>> GetAllCatWithPicture();
    }
}
