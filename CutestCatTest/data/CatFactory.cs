using CutestCat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CutestCatTest.data
{
    public static class CatFactory
    {
        public static List<Cat> GetAllCandidates()
        {
            return new List<Cat>()
           {
               new Cat(){ Reference = "Ugly", Url= "ugly.com" },
               new Cat(){ Reference = "veryUgly",  Url= "veryUgly.com" },
               new Cat(){ Reference = "cute", Url= "cute.com" },
               new Cat(){ Reference = "veryCute",  Url= "veryCute.com" }

           };
        }

        public static List<Cat> GetCats()
        {
            return new List<Cat>()
           {
               new Cat(){ Reference = "Ugly", Url= "ugly.com", LostVoteCount = 10, WinVoteCount = 1 },
               new Cat(){ Reference = "veryUgly",  Url= "veryUgly.com", LostVoteCount = 180, WinVoteCount = 1 },
               new Cat(){ Reference = "cute", Url= "cute.com", LostVoteCount = 3, WinVoteCount = 17 },
               new Cat(){ Reference = "veryCute",  Url= "veryCute.com", LostVoteCount = 1, WinVoteCount = 81 }

           };
        }
    }
}
