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

        public ActionResult QuickCreate(string searchText)
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var model = new SearchViewModel
                {
                    AnyOfTheseWords = searchText.Trim().ToLower(),
                    Title = searchText.Trim().Length > 10 ? searchText.Trim().Substring(0, 7) + "..." : searchText.Trim().ToLower()
                };
                var entity = Mapper.Map<Search>(model);
                _searchLogic.Insert(entity);   
            }
            return Content(bool.TrueString);
        }
	}
}