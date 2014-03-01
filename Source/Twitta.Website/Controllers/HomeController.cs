using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using StackExchange.Profiling;
using TweetSharp;
using Twitta.Website.Logic;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITweetsLogic _tweetsLogic;
        private readonly ISearchLogic _searchLogic;
        private readonly ITwitterSearchRepository _twitterSearchRepository;
        private readonly ITwitterAppRepository _twitterAppRepository;
        private readonly ITweetsRepository _tweetsRepository;
        private readonly TweetProcessor _tweetProcessor = new TweetProcessor();
        public HomeController(
            ITweetsLogic tweetsLogic,
            ISearchLogic searchLogic,
            ITwitterSearchRepository twitterSearchRepository,
            ITwitterAppRepository twitterAppRepository,
            ITweetsRepository tweetsRepository)
        {
            _tweetsLogic = tweetsLogic;
            _searchLogic = searchLogic;
            _twitterSearchRepository = twitterSearchRepository;
            _twitterAppRepository = twitterAppRepository;
            _tweetsRepository = tweetsRepository;
        }

        /// <summary>
        /// Lists the initial searches available. very wow.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = _searchLogic.GetItems();
            foreach (var item in model)
            {
                item.Tweets = _tweetsLogic.GetTweetsInDateRange(item.SearchId, DateTime.UtcNow.AddHours(-4), DateTime.UtcNow);
            }
            return View(model);
        }

        public ActionResult KeepAlive()
        {
            return View();
        }
        public ActionResult SearchResults(int id, DateTime? startDate, DateTime? endDate)
        {
            //List<Tweet> tweets;
            string tweets;
            IEnumerable<dynamic> fancyWordStats;
            startDate = startDate ?? DateTime.UtcNow.AddHours(-4);
            endDate = endDate ?? DateTime.UtcNow;
            var profiler = MiniProfiler.Current; // it's ok if this is null

            //tweets = _tweetsRepository.GetTweetsInDateRange(id, (DateTime)startDate, DateTime.UtcNow);
            tweets = _tweetsRepository.GetTweetTextInDateRange(id, (DateTime)startDate, (DateTime)endDate);
            using (profiler.Step("3"))
            {
                //var temp = _tweetProcessor.WordCountStats(tweets.Select(st => st.Text).ToList());
                var temp = _tweetProcessor.WordCountStats(tweets);
                profiler.Step("4");
                var temp2 = temp.Where(i => i.Value > 2 && i.Key.Length > 2).OrderByDescending(f => f.Value);
                profiler.Step("5");
                fancyWordStats = temp2.Select(i => new { word = i.Key, total = i.Value, searchId = id });
                profiler.Step("6");
            }

            //var tweets = _tweetsRepository.GetTweetsInDateRange(id, (DateTime)startDate, (DateTime)endDate);
            //var fancyWordStats = _tweetProcessor.WordCountStats(tweets.Select(st => st.Text).ToList())
            //    .Where(i => i.Value > 2 && i.Key.Length > 2).OrderByDescending(f => f.Value)
            //    .Select(i => new { word = i.Key, total = i.Value, searchId = id });

            return View("SearchResults", fancyWordStats);
        }

        public JsonResult RecentTweets(int id, string word, DateTime? startDate, DateTime? endDate)
        {
            startDate = startDate ?? DateTime.UtcNow.AddHours(-4);
            endDate = endDate ?? DateTime.UtcNow;
            var tweets = _tweetsRepository.GetTweetsInDateRange(id, (DateTime)startDate, (DateTime)endDate)
                .Where(t => t.Text.ToLower().Contains(word.ToLower()));

            var minimalTweets = tweets.Select(i => new
            {
                i.TwitterUserId,
                TwitterUserScreenName = i.User.ScreenName,
                TwitterUserProfileImage = i.User.ProfileImageUrl,
                i.Text,
                i.CreatedDate,
                id = i.Id.ToString()
            });

            return Json(minimalTweets);
        }

        public ActionResult TestPersist()
        {
            var app = _twitterAppRepository.GetItem(1);
            var results = _twitterSearchRepository.Search("ecampus", 0, app);
            var tweets = Mapper.Map<List<TwitterStatus>, List<Tweet>>(results.SearchResult.Statuses.ToList());
            _tweetsLogic.PersistTweets(tweets, 0);
            return Content("done");
        }


        public ActionResult TestSyphon()
        {
            var app = _twitterAppRepository.GetItem(1);
            var queries = new List<string> { "pharma", "bayer", "mylan", "merck", "johnson" };
            foreach (var query in queries)
            {
                var results = GetTweets(query, app);
                var tweets = Mapper.Map<List<TwitterStatus>, List<Tweet>>(results.SearchResult.Statuses.ToList());
                _tweetsLogic.PersistTweets(tweets, 0);
            }
            //return total records and number just added?
            return Content("done");
        }

        private SearchResultWithRateLimit GetTweets(string query, TwitterApp app)
        {
            if (HttpContext.Application[query + "Last"] == null)
                HttpContext.Application[query + "Last"] = 10;
            var results = _twitterSearchRepository.Search(query, Convert.ToInt64(HttpContext.Application[query + "Last"]), app);
            HttpContext.Application[query + "Last"] = results.LastId;
            return results;
        }
    }
}
