using System;
using System.Collections.Generic;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Logic.Implementations
{
    public class TweetsLogic : ITweetsLogic
    {
        private readonly ITweetsRepository _tweetsRepository;

        public TweetsLogic(ITweetsRepository tweetsRepository)
        {
            _tweetsRepository = tweetsRepository;
        }


        public void PersistTweets(List<Tweet> tweets, long searchId)
        {
            foreach (var tweet in tweets)
                tweet.SearchId = searchId;
            _tweetsRepository.BulkInsert(tweets);
        }

        public List<Tweet> GetList(long searchId)
        {
            return _tweetsRepository.GetList(searchId);
        }

        public List<Tweet> GetTweetsInDateRange(long searchId, DateTime startDate, DateTime endDate)
        {
            return _tweetsRepository.GetTweetsInDateRange(searchId, startDate, endDate);
        }

        public string GetTweetTextInDateRange(long searchId, DateTime startDate, DateTime endDate)
        {
            return _tweetsRepository.GetTweetTextInDateRange(searchId, startDate, endDate);
        }
    }
}