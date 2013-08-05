using System.Web.Mvc;
using News.Repository;

namespace News.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _repo;

        public NewsController(INewsRepository repo)
        {
            _repo = repo;
        }
    }
}
