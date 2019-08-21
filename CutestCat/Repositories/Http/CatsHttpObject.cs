using CutestCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutestCat.Repositories.Http
{
    public class CatsHttpObject
    {
        public List<CatHttpObject> Images { get; set; }
    }

    public class CatHttpObject
    {
        public string Url { get; set; }
        public string Id { get; set; }

        public Cat ToModel()
        {
            return new Cat()
            {
                Reference = Id,
                Url = Url
            };
        }
    }
}
