using System.Collections.Generic;

namespace News.Models
{
    public class NewsItem
    {
        public long Id { get; set; }

        public string Url { get; set; }
        public string Title { get; set; }

        public uint Votes { get; set; }

        public int SubmittedBy { get; set; }

        //public IList<Comment> Comments { get; set; }
    }
}