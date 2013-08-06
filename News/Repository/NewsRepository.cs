using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            conn.Open();
            conn.Execute("insert into Items(url, title, submitted_by) values(@url, @title, @by)",
                new {url = item.Url, title = item.Title, by = item.SubmittedBy});
            conn.Close();
        }

        public IEnumerable<NewsItem> GetItems()
        {
            conn.Open();
            var result = conn.Query<NewsItem>("select * from Items");
            conn.Close();

            return result;
        }

        public bool IsUrlUniqe(string url)
        {
            return true;
        }

        public void Vote(int itemId, int userId)
        {
            throw new NotImplementedException();
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