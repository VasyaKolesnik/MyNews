using News.Interface;
using News.Models;
using System.Linq;
using System.Web.Mvc;

namespace News.Controllers
{
    public class HomeController : Controller
    {
        NewsContext db = new NewsContext();
        private readonly IViewService _viewService;

        public HomeController(IViewService viewService)
        {
            _viewService = viewService;
        }

        public ActionResult Index()
        {
            ViewBag.Categories = _viewService.GetCategory();
            var news = _viewService.GetSixNews();
            return View(news.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Category = _viewService.GetCategorySelect();
            return View();
        }

        [HttpPost]
        public ActionResult Create(FakeNewsViewModel model)
        {
            string path = Server.MapPath("/Content/img/");
            int id = _viewService.Create(model, path);
            return RedirectToAction("NewsView", new { Id = id });
        }

        
        [HttpGet]
        public ActionResult CreateCat()
        {
            ViewBag.Category = _viewService.GetCategorySelect();
            return View();
        }
        [HttpPost]
        public ActionResult CreateCat(Category categories)
        {
            _viewService.CreateCat(categories);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FakeNewsViewModel fakeNews = _viewService.ViewNewsEdit(id);
            if (fakeNews != null)
            {
                ViewBag.Category = _viewService.GetCategorySelect();
                return View(fakeNews);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(FakeNewsViewModel fakeNews)
        {
            string path = Server.MapPath("/Content/img/");
            _viewService.Edit(fakeNews, path);
            return RedirectToAction("NewsView", new { id = fakeNews.Id });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            FakeNews b = _viewService.ViewNews(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            FakeNews fakeNews = _viewService.ViewNews(id);
            if (fakeNews == null)
            {
                return HttpNotFound();
            }
            _viewService.Delete(fakeNews);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CategoryViews(int? id)
        {
            ViewBag.Categories = _viewService.GetCategory();
            
            return View(_viewService.CategoryViews(id));
        }

        [HttpGet]
        public ActionResult NewsView(int? id)
        {
            ViewBag.Categories = _viewService.GetCategory();
            if (id == null)
            {
                return HttpNotFound();
            }
            var fakeNews = _viewService.ViewNews(id);
            if (fakeNews == null)
            {
                return HttpNotFound();
            }
            return View(fakeNews);
        }

    }
}