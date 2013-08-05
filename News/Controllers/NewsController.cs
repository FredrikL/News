using System.Net;
using System.Web.Mvc;
using News.Models;
using News.Models.Dto;
using News.Repository;

namespace News.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUserRepository _userRepository;

        public NewsController(INewsRepository newsRepository, IUserRepository userRepository)
        {
            _newsRepository = newsRepository;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View(_newsRepository.GetItems());
        }

        [HttpPost]
        public ActionResult Submit(SubmitItemDto item)
        {
            if (!_userRepository.IsLoggedIn())
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            if(string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Url))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if(!_newsRepository.IsUrlUniqe(item.Url))
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable); //TODO: for now

            _newsRepository.Add(new NewsItem() { Title = item.Title, Url = item.Url, SubmittedBy = _userRepository.GetCurrentUserId() });
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Vote(VoteDto vote)
        {
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
