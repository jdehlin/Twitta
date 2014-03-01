using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Repositories
{
    public class TweetsRepository : ITweetsRepository
    {
        private readonly IUsersRepository _usersRepository;

        public TweetsRepository(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }


        public Tweet GetItem(long tweetId)
        {
            const string sql = "SELECT * FROM Tweets WHERE TweetId = @tweetId";
            using (var connection = Utilities.Database.GetProfiledOpenConnection())
            {
                return connection.Query<Tweet>(sql, new {tweetId}).FirstOrDefault();
            }
        }

        public List<Tweet> GetList(long searchId)
        {
            const string sql = "SELECT * FROM Tweets WHERE SearchId = @searchId";
            using (var connection = Utilities.Database.GetProfiledOpenConnection())
            {
                return connection.Query<Tweet>(sql, new { searchId }).ToList();
            }
        }

        public void BulkInsert(List<Tweet> tweets)
        {
            using (var connection = Utilities.Database.GetProfiledOpenConnection())
            {
                foreach (var tweet in tweets)
                {
                    var duplicate = GetItem(tweet.TweetId);
                    if (duplicate == null)
                    {
                        tweet.TwitterUserId = tweet.User.UserId;
                        connection.Insert(tweet);
                        _usersRepository.Insert(tweet.User);
                    }
                }
            }
        }

        public List<Tweet> GetTweetsInDateRange(long searchId, DateTime startDate, DateTime endDate)
        {
            const string sql = @"SELECT dbo.Tweets.TweetId, dbo.Tweets.IdStr, dbo.Tweets.TwitterUserId, dbo.Tweets.InReplyToScreenName, dbo.Tweets.InReplyToStatusId, dbo.Tweets.InReplyToUserId, dbo.Tweets.IsFavorited, 
                         dbo.Tweets.IsTruncated, dbo.Tweets.Source, dbo.Tweets.Text, dbo.Tweets.Language, dbo.Tweets.IsPossiblySensitive, dbo.Tweets.RetweetCount, dbo.Tweets.CreatedDate, dbo.Tweets.SearchId, 
                         dbo.Users.ScreenName, dbo.Users.FollowersCount, dbo.Users.Name
FROM            dbo.Tweets left Outer JOIN
                         dbo.Users ON dbo.Tweets.TwitterUserId = dbo.Users.UserId
WHERE SearchId = @searchId AND dbo.Tweets.CreatedDate between @startDate and @endDate";
            using (var connection = Utilities.Database.GetProfiledOpenConnection())
            {
                return connection.Query<Tweet, User, Tweet>(sql, (t,u) =>
                {
                    t.User = u;
                    return t;
                }, splitOn: "ScreenName", param: new {searchId, startDate, endDate}).ToList();
            }
        }
    }
}