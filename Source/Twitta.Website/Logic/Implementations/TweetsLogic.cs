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
    }
}