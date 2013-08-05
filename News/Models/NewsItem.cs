using System.Collections.Generic;

namespace News.Models
{
    public class NewsItem
    {
        public string Url { get; set; }
        public string Title { get; set; }

        public uint Votes { get; set; }

        //public IList<Comment> Comments { get; set; }
    } 
}