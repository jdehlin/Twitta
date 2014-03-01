using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using StackExchange.Profiling;
using StructureMap.Query;
using Twitta.Website.Logic;
using Twitta.Website.Models;
using Twitta.Website.ViewModels;

namespace Twitta.Website.Controllers
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

        public ActionResult TempGraphs(long id)
        {
            return View(_searchLogic.GetItem(id));
        }

        //
        // GET: /Reports/BasicWordCountData/5

        public JsonResult BasicWordCountData(int id)
        {
            var processor = new TweetProcessor();
            var tweets = _tweetsLogic.GetList(id);
            var words = processor.WordCountStats(tweets.Select(t => t.Text).ToList()).OrderByDescending(w => w.Value).Take(20).ToList();
            var dataModel = new { words = words.Select(w => w.Key), counts = words.Select(w => w.Value) };
            return new JsonResult
            {
                Data = dataModel,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AdvancedWordCountData(int id, int interval, DateTime? startRange, DateTime? endRange)
        {
            if (startRange == null)
                startRange = DateTime.UtcNow.AddHours(-4);
            if (endRange == null)
                endRange = DateTime.UtcNow;
            var processor = new TweetProcessor();
            var tweets = _tweetsLogic.GetList(id);
            var timeInterval = (int)Math.Floor((endRange - startRange).Value.TotalMilliseconds / interval);
            var counts = new List<List<int>>();
            var inset = 0;
            var texts = tweets.Where(t => t.CreatedDate > endRange.Value.AddMilliseconds(-timeInterval) && t.CreatedDate < endRange).Select(t => t.Text).ToList();
            var result = processor.WordCountStats(texts).OrderByDescending(w => w.Value).Take(10).ToList();
            var categories = result.Select(w => w.Key).ToList();
            foreach (var category in categories)
            {
                var innerCounts = new List<int>();
                for (var i = 0; i < interval; i++)
                {
                    var intervalStartRange = startRange.Value.AddMilliseconds(inset);
                    var intervalEndRange = startRange.Value.AddMilliseconds(inset + timeInterval);
                    texts = tweets.Where(t => t.CreatedDate > intervalStartRange && t.CreatedDate < intervalEndRange).Select(t => t.Text).ToList();
                    var wordsDict = processor.WordCountStats(texts);
                    innerCounts.Add(wordsDict.ContainsKey(category) ? wordsDict[category] : 0);
                    inset += timeInterval;
                }
                counts.Add(innerCounts);
                inset = 0;
            }
            //for (var i = 0; i < interval; i++)
            //{
            //    var intervalStartRange = startRange.Value.AddMilliseconds(inset);
            //    var intervalEndRange = startRange.Value.AddMilliseconds(inset + timeInterval);
            //    texts = tweets.Where(t => t.CreatedDate > intervalStartRange && t.CreatedDate < intervalEndRange).Select(t => t.Text).ToList();
            //    var wordsDict = processor.WordCountStats(texts);
            //    var innerCounts = new List<int>();
            //    foreach (var category in categories)
            //        innerCounts.Add(wordsDict[category]);
            //    counts.Add(innerCounts);
            //    inset += timeInterval;
            //}
            var dataModel = new { words = categories, counts };
            return new JsonResult
            {
                Data = dataModel,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
