using System.Web.Mvc;
using AutoMapper;
using Twitta.Website.Logic;
using Twitta.Website.Models;
using Twitta.Website.ViewModels;

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
            return View(new TwitterAppViewModel());
        }

        [HttpPost]
        public ActionResult Create(TwitterAppViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.Map<TwitterApp>(model);
                _apiApplicationLogic.SaveOrUpdate(entity);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View(Mapper.Map<TwitterAppViewModel>(_apiApplicationLogic.GetItem(id)));
        }

        [HttpPost]
        public ActionResult Edit(TwitterAppViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.Map<TwitterApp>(model);
                _apiApplicationLogic.SaveOrUpdate(entity);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _apiApplicationLogic.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(_apiApplicationLogic.GetItem(id));
        }
	}
}