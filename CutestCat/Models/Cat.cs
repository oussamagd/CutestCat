using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutestCat.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int LostVoteCount { get; set; }
        public int WinVoteCount { get; set; }

        public int Score
        {
            get
            {
                return WinVoteCount - LostVoteCount;
            }
        }

    }
}
