using CutestCat.Models;

namespace CutestCat.ViewModel
{
    public class CadidateCatViewModel
    {
        public string reference { get; set; }

        public string Url { get; set; }

        public CadidateCatViewModel()
        {

        }

        public CadidateCatViewModel(Cat cat)
        {
            reference = cat.Reference;
            Url = cat.Url;
        }
    }
}
