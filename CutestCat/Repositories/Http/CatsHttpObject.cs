using CutestCat.Models;
using System.Collections.Generic;

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
