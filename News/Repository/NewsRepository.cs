using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
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
            conn.Open();
            var result = conn.Query<NewsItem>("select * from Items");
            conn.Close();

            return result;
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