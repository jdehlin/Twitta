using System.Collections.Generic;
using System.Web.Mvc;
using StackExchange.Exceptional;

namespace Twitta.Website.Controllers
{
    using AutoMapper;
    using ViewModels;

    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        //
        // GET: /TestData/
        public ActionResult Index()
        {
            
            return View();
        }

        

    }
}
