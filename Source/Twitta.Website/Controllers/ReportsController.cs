using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Twitta.Website.Logic;
using Twitta.Website.Logic.Implementations;
using Twitta.Website.Models;

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

        public JsonResult BasicWordCountData(int id, DateTime? startRange, DateTime? endRange)
        {
            //var start = !String.IsNullOrWhiteSpace(startRange) ? DateTime.Parse(startRange) : DateTime.Now.AddHours(-4);
            if (startRange == null)
                startRange = DateTime.UtcNow.AddHours(-4);
            else
                startRange = startRange.Value.ToUniversalTime();
            if (endRange == null)
                endRange = DateTime.UtcNow;
            else
                endRange = endRange.Value.ToUniversalTime();
            var processor = new TweetProcessor();
            var tweetsText = _tweetsLogic.GetTweetTextInDateRange(id, startRange.Value, endRange.Value);
            var words = processor.WordCountStats(tweetsText).OrderByDescending(w => w.Value).Take(20).ToList();
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
            else
                startRange = startRange.Value.ToUniversalTime();
            if (endRange == null)
                endRange = DateTime.UtcNow;
            else
                endRange = endRange.Value.ToUniversalTime();
            var processor = new TweetProcessor();
            var tweets = _tweetsLogic.GetTweetsInDateRange(id, startRange.Value, endRange.Value);
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
            var dataModel = new { words = categories, counts, timeInterval, startRange };
            return new JsonResult
            {
                Data = dataModel,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SentimentData(int id, int interval, DateTime? startRange, DateTime? endRange)
        {
            if (startRange == null)
                startRange = DateTime.UtcNow.AddHours(-4);
            if (endRange == null)
                endRange = DateTime.UtcNow;
            var tweets = _tweetsLogic.GetTweetsInDateRange(id, startRange.Value, endRange.Value);
            var timeInterval = (int)Math.Floor((endRange - startRange).Value.TotalMilliseconds / interval);
            var counts = new List<List<int>>();
            var inset = 0;
            var categories = new List<SentimentOptions> { SentimentOptions.Positive, SentimentOptions.Negative, SentimentOptions.Neutral };
            foreach (var category in categories)
            {
                var currentCategory = category;
                var innerCounts = new List<int>();
                for (var i = 0; i < interval; i++)
                {
                    var intervalStartRange = startRange.Value.AddMilliseconds(inset);
                    var intervalEndRange = startRange.Value.AddMilliseconds(inset + timeInterval);
                    var texts = tweets.Where(t => t.CreatedDate > intervalStartRange && t.CreatedDate < intervalEndRange).Select(t => t).ToList();
                    var rangeSentimentCount = texts.Count(t => t.Sentiment == currentCategory);
                    innerCounts.Add(rangeSentimentCount);
                    inset += timeInterval;
                }
                counts.Add(innerCounts);
                inset = 0;
            }
            var dataModel = new { words = categories, counts, timeInterval };
            return new JsonResult
            {
                Data = dataModel,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
