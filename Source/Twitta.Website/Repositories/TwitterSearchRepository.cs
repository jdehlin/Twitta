using System.Linq;
using TweetSharp;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Repositories
{
    public class TwitterSearchRepository : ITwitterSearchRepository
    {


        public SearchResultWithRateLimit Search(string queryTerm, int resultType, long sinceId, TwitterApp keys)
        {
            SearchOptions options = new SearchOptions
            {
                Q = System.Web.HttpUtility.UrlEncode(queryTerm),
                Count = 100,
                Lang = "en",
                Resulttype = (TwitterSearchResultType)resultType,
                SinceId = sinceId
            };

            var service = new TwitterService(keys.ConsumerKey, keys.ConsumerKeySecret);
            service.AuthenticateWith(keys.Token, keys.TokenSecret);
            var result = new SearchResultWithRateLimit();
            result.SearchResult = service.Search(options);
            result.RateLimitStatus = service.Response.RateLimitStatus;
            if (result.SearchResult.Statuses.Any())
            {
                result.LastId = result.SearchResult.Statuses.OrderByDescending(f => f.Id).FirstOrDefault().Id;
            }
            else
            {
                result.LastId = options.SinceId.Value;
            }

            return result;
        }
        public SearchResultWithRateLimit Search(SearchOptions options, TwitterApp keys)
        {
            var service = new TwitterService(keys.ConsumerKey, keys.ConsumerKeySecret);
            service.AuthenticateWith(keys.Token, keys.TokenSecret);
            var result = new SearchResultWithRateLimit();
            result.SearchResult = service.Search(options);
            result.RateLimitStatus = service.Response.RateLimitStatus;
            if (result.SearchResult.Statuses.Any())
            {
                result.LastId = result.SearchResult.Statuses.OrderByDescending(f => f.Id).FirstOrDefault().Id;
            }
            else
            {
                result.LastId = options.SinceId.Value;
            }

            return result;
        }
        public SearchResultWithRateLimit Search(string query, long sinceid, TwitterApp keys)
        {
            var service = new TwitterService(keys.ConsumerKey, keys.ConsumerKeySecret);
            service.AuthenticateWith(keys.Token, keys.TokenSecret);
            var options = new SearchOptions
            {
                Q = System.Web.HttpUtility.UrlEncode(query),
                Count = 100,
                Lang = "en",
                Resulttype = TwitterSearchResultType.Mixed,
                SinceId = sinceid
            };

            // Geocode = new TwitterGeoLocationSearch(39.6456, -79.9433, 30, TwitterGeoLocationSearch.RadiusType.Mi),

            var result = new SearchResultWithRateLimit();
            result.SearchResult = service.Search(options);
            result.RateLimitStatus = service.Response.RateLimitStatus;
            if (result.SearchResult.Statuses.Any())
            {
                result.LastId = result.SearchResult.Statuses.OrderByDescending(f => f.Id).FirstOrDefault().Id;
            }
            else
            {
                result.LastId = sinceid;
            }

            return result;
        }
    }
}