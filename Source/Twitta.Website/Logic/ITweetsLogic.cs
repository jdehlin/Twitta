using System;
using System.Collections.Generic;
using Twitta.Website.Models;

namespace Twitta.Website.Logic
{
    public interface ITweetsLogic
    {
        void PersistTweets(List<Tweet> tweets, long searchId);
        List<Tweet> GetList(long searchId);

        List<Tweet> GetTweetsInDateRange(long searchId, DateTime startDate, DateTime endDate);
        string GetTweetTextInDateRange(long searchId, DateTime startDate, DateTime endDate);
    }
}
