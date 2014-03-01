using System;
using System.Collections.Generic;
using Twitta.Website.Models;

namespace Twitta.Website.RepositoryInterfaces
{
    public interface ITweetsRepository
    {
        Tweet GetItem(long tweetId);
        List<Tweet> GetList(long searchId); 
        void BulkInsert(List<Tweet> tweets);

        List<Tweet> GetTweetsInDateRange(long searchId, DateTime startDate, DateTime endDate);
        string GetTweetTextInDateRange(long searchId, DateTime startDate, DateTime endDate);
    }
}
