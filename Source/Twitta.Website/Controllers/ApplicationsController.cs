using System.Web.Mvc;
using Twitta.Website.Logic;
using Twitta.Website.Models;

namespace Twitta.Website.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly ITwitterAppLogic _apiApplicationLogic;

        public ApplicationsController(ITwitterAppLogic apiApplicationLogic)
        {
            _apiApplicationLogic = apiApplicationLogic;
        }


        public ActionResult Index()
        {
            return View(_apiApplicationLogic.GetItems());
        }

        public ActionResult Create()
        {
            return View(new TwitterApp());
        }

        [HttpPost]
        public ActionResult Create(TwitterApp model)
        {
            if (ModelState.IsValid)
            {
                _apiApplicationLogic.SaveOrUpdate(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View(_apiApplicationLogic.GetItem(id));
        }

        [HttpPost]
        public ActionResult Edit(TwitterApp model)
        {
            if (ModelState.IsValid)
            {
                _apiApplicationLogic.SaveOrUpdate(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View(_apiApplicationLogic.GetItem(id));
        }
	}
}