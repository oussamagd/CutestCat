﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutestCat.Models;

namespace CutestCat.Repositories.Sql
{
    public interface ICatRepository
    {
        List<Cat> GetCats();
    }
}
