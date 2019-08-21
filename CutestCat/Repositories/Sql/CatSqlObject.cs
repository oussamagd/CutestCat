using CutestCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutestCat.Repositories.Sql
{
    public class CatSqlObjet
    {
        public string Reference { get; set; }
        public string Url { get; set; }
        public int LostVoteCount { get; set; }
        public int WinVoteCount { get; set; }

        public Cat ToModel()
        {
            return new Cat()
            {
                Reference = Reference,
                Url = Url,
                LostVoteCount = LostVoteCount,
                WinVoteCount = WinVoteCount
            };
        }
    }
}
