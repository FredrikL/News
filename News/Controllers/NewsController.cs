using System.Net;
using System.Web.Mvc;
using News.Models;
using News.Models.Dto;
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

        public ActionResult Index()
        {
            return View(_repo.GetItems());
        }

        [HttpPost]
        public ActionResult Submit(SubmitItemDto item)
        {
            if(string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Url))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _repo.Add(new NewsItem() {Title = item.Title, Url = item.Url});
            return RedirectToAction("Index");
        }
    }
}
