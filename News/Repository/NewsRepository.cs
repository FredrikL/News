using System.Collections.Generic;
using News.Models;

namespace News.Repository
{
    public interface INewsRepository
    {
        void Add(NewsItem item);
        IList<NewsItem> GetItems();
    }

    public class NewsRepository : INewsRepository
    {
        public void Add(NewsItem item)
        {
            throw new System.NotImplementedException();
        }

        public IList<NewsItem> GetItems()
        {
            throw new System.NotImplementedException();
        }
    }
}