namespace CutestCat.Models
{
    public class Cat
    {
        public string Reference { get; set; }
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
