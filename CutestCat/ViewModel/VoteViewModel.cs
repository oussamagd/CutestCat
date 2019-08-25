using CutestCat.Models;

namespace CutestCat.ViewModel
{
    public class VoteViewModel
    {
        public CadidateCatViewModel WinnerCat { get; set; }
        public CadidateCatViewModel LoserCat { get; set; }

        public VoteModel ToModel()
        {
            return new VoteModel
            {
                WinnerCat = new Cat() { Url = WinnerCat.Url, Reference = WinnerCat.reference },
                LoserCat = new Cat() { Url = LoserCat.Url, Reference = LoserCat.reference }
            };
        }
    }
}
