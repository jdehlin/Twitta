using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using TweetSharp;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;


namespace Twitta.Website.Logic.Implementations
{
    public class SearchJobLogic : ISearchJobLogic
    {
        private readonly ITwitterAppLogic _twitterAppLogic;
        private readonly ISearchLogic _searchLogic;
        private readonly ITweetsLogic _tweetsLogic;
        private readonly ISearchHistoryLogRepository _searchHistoryLogicRepository;
        private readonly ITwitterSearchRepository _twitterSearchRepository;

        public SearchJobLogic(
            ITwitterAppLogic twitterAppLogic,
            ISearchLogic searchLogic,
            ITweetsLogic tweetsLogic,
            ISearchHistoryLogRepository searchHistoryLogicRepository,
            ITwitterSearchRepository twitterSearchRepository
            )
        {
            _twitterAppLogic = twitterAppLogic;
            _searchLogic = searchLogic;
            _tweetsLogic = tweetsLogic;
            _searchHistoryLogicRepository = searchHistoryLogicRepository;
            _twitterSearchRepository = twitterSearchRepository;
            
        }

        public void RunAllSearches()
        {
            var appinfo = _twitterAppLogic.GetItem(1);
            if (appinfo.RateLimitRemaining < 10)
            {
                //wait 65 seconds so that hopefully everything is all reset and happy and such as
                Thread.Sleep(65000);
            }

            foreach (var search in _searchLogic.GetItems(SearchDependencies.Logs))
            {
                //search tweets
                var results = _twitterSearchRepository.Search(search.ToQuery(), search.ResultType, search.LastLog == null ? 0 : search.LastLog.LastTweetId, appinfo);

                //add a record to the history log 
                var lastlog = new SearchHistoryLog() { LastTweetId = results.LastId, SearchDate = DateTime.UtcNow, SearchId = search.SearchId, TweetCount = results.SearchResult.Statuses.Count() };
                _searchHistoryLogicRepository.Insert(lastlog);

                //update app info with last rate limit status
                appinfo.LastAccessedDTM = DateTime.UtcNow;
                appinfo.RateLimitRemaining = results.RateLimitStatus.HourlyLimit = results.RateLimitStatus.RemainingHits;
                _twitterAppLogic.SaveOrUpdate(appinfo);


                //map tweets to domain model
                var tweets = Mapper.Map<List<TwitterStatus>, List<Tweet>>(results.SearchResult.Statuses.ToList());


                //persist tweets to database
                _tweetsLogic.PersistTweets(tweets, search.SearchId);
            }
        }
    }
}