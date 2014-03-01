using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using TweetSharp;

namespace Twitta.Website.Models
{
    public class SearchResultWithRateLimit
    {

        public TwitterSearchResult SearchResult { get; set; }
        public TwitterRateLimitStatus RateLimitStatus { get; set; }
        public long LastId { get; set; }
    }

}