using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Repositories
{
    public class SearchHistoryLogRepository : ISearchHistoryLogRepository
    {
        private DbConnection _connection;

        public IList<SearchHistoryLog> GetItems(long searchId)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                var result = _connection.GetList<SearchHistoryLog>(new { SearchId = searchId });
                return result.ToList();
            }
        }

        public IList<SearchHistoryLog> GetItems(IList<long> searchIds)
        {
            IList<SearchHistoryLog> results;
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                const string query = "SELECT * FROM SearchHistoryLogs WHERE SearchId IN @SearchIds";
                results = _connection.Query<SearchHistoryLog>(query, new {SearchIds = searchIds.ToArray()}).ToList();
            }
            return results;
        }

        public int Insert(SearchHistoryLog entity)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                const string query = "INSERT INTO SearchHistoryLogs (SearchId, SearchDate, LastTweetId, TweetCount) VALUES (@SearchId, @SearchDate, @LastTweetId,@TweetCount)";
                return _connection.Execute(query, entity);
            }
        }
    }
}