using System.Web.Mvc;

namespace Twitta.Website.Controllers
{
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


        public ActionResult KeepAlive()
        {
            return View();
        }
        

    }
}
