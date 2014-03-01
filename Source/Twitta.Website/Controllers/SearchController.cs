using System.Web.Mvc;
using AutoMapper;
using Twitta.Website.Logic;
using Twitta.Website.Models;
using Twitta.Website.ViewModels;

namespace Twitta.Website.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchLogic _searchLogic;

        public SearchController(ISearchLogic searchLogic)
        {
            _searchLogic = searchLogic;
        }

        public ActionResult Index()
        {
            return View(_searchLogic.GetItems(SearchDependencies.Logs));
        }

        public ActionResult Create()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        public ActionResult Create(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.Map<Search>(model);
                _searchLogic.Insert(entity);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View(Mapper.Map<SearchViewModel>(_searchLogic.GetItem(id)));
        }

        [HttpPost]
        public ActionResult Edit(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.Map<Search>(model);
                _searchLogic.Update(entity);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(long id)
        {
            _searchLogic.Delete(id);
            return RedirectToAction("Index");
        }
	}
}