using System.Linq;
using System.Web.Mvc;
using Twitta.Website.Logic;
using Twitta.Website.Models;

namespace TwitterSandbox.Website.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ITweetsLogic _tweetsLogic;
        private readonly ISearchLogic _searchLogic;

        public ReportsController(ITweetsLogic tweetsLogic, ISearchLogic searchLogic)
        {
            _tweetsLogic = tweetsLogic;
            _searchLogic = searchLogic;
        }


        //
        // GET: /Reports/Search/5

        //public ActionResult TempGraphs(long id)
        //{
        //    var processor = new TweetProcessor();
        //    var tweets = _tweetsLogic.GetList(id);
        //    var words = processor.WordCountStats(tweets.Select(t => t.Text).ToList()).Where(w => w.Value > 1).OrderByDescending(w => w.Value).Take(20);
        //    var viewModel = new SearchReportView
        //    {
        //        Search = _searchLogic.GetItem(id),
        //        Data = words
        //    };
        //    return View(viewModel);
        //}

        //
        // GET: /Reports/BasicWordCountData/5

        public JsonResult BasicWordCountData(int id)
        {
            var processor = new TweetProcessor();
            var tweets = _tweetsLogic.GetList(id);
            var words = processor.WordCountStats(tweets.Select(t => t.Text).ToList()).Where(w => w.Value > 1).OrderByDescending(w => w.Value).Take(20).ToList();
            var dataModel = new { words = words.Select(w => w.Key), counts = words.Select(w => w.Value) };
            return new JsonResult
            {
                Data = dataModel,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
