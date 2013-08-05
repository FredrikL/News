using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using News.Models;

namespace News.Repository
{
    public class NewsRepository : INewsRepository, IDisposable
    {
        private SqlConnection conn;

        public NewsRepository()
        {
            this.conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        }

        public void Add(NewsItem item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<NewsItem> GetItems()
        {
            return Enumerable.Empty<NewsItem>();
        }

        public void Dispose()
        {
            if (this.conn != null)
            {
                this.conn.Dispose();
                this.conn = null;
            }
        }
    }
}