using System.Collections.Generic;
using News.Models;

namespace News.Repository
{
    public interface INewsRepository
    {
        void Add(NewsItem item);
        IEnumerable<NewsItem> GetItems();
        bool IsUrlUniqe(string url);
    }
}