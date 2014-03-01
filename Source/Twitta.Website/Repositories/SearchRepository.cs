using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Repositories
{
    public class SearchRepository : ISearchRepository
    {

        private readonly ISearchHistoryLogRepository _searchHistoryLogRepository;
        private DbConnection _connection;

        public SearchRepository(ISearchHistoryLogRepository searchHistoryLogRepository)
        {
            _searchHistoryLogRepository = searchHistoryLogRepository;
        }

        public IList<Search> GetItems(SearchDependencies? dependencies = null)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                var result = _connection.GetList<Search>().ToList();
                if (result.Any() && dependencies != null)
                {
                    if ((dependencies & SearchDependencies.Logs) == SearchDependencies.Logs)
                    {
                        List<SearchHistoryLog> Logs = null;
                        var searchIds = (from s in result let searchId = s.SearchId select searchId).ToList();
                        if (searchIds.Any())
                        {
                            Logs = _searchHistoryLogRepository.GetItems(searchIds).ToList();
                        }

                        if (Logs != null && Logs.Any())
                        {
                            foreach (var search in result)
                            {
                                search.Logs = Logs.FindAll(s => s.SearchId == search.SearchId).ToList();
                                search.LastLog = search.Logs.OrderByDescending(l => l.SearchDate).FirstOrDefault();
                            }
                        }
                    }
                }
                return result.ToList();
            }
        }

        public Search GetItem(long id, SearchDependencies? dependencies = null)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                var entity = _connection.Get<Search>(id);
                if (entity != null)
                {
                    if (dependencies != null)
                    {
                        entity.Logs = _searchHistoryLogRepository.GetItems(entity.SearchId);
                        if (entity.Logs.Any())
                        {
                            entity.LastLog = entity.Logs.OrderByDescending(l => l.SearchDate).First();
                        }
                    }
                }
                return entity;
            }
        }

        public int Insert(Search entity)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                const string query = "INSERT INTO Searches (Title, AllOfTheseWords, ThisExactPhrase, AnyOfTheseWords, NoneOfTheseWords, NearThisPlace, Radius, ResultType) " +
                                     "VALUES (@Title, @AllOfTheseWords, @ThisExactPhrase, @AnyOfTheseWords, @NoneOfTheseWords, @NearThisPlace, @Radius, @ResultType)";
                return _connection.Execute(query, entity);
            }
        }

        public void Update(Search entity)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                const string query = "UPDATE Searches SET Title = @Title, AllOfTheseWords = @AllOfTheseWords, ThisExactPhrase = @ThisExactPhrase, AnyOfTheseWords = @AnyOfTheseWords, NoneOfTheseWords = @NoneOfTheseWords, NearThisPlace = @NearThisPlace, Radius = @Radius, ResultType = @ResultType WHERE SearchId = @SearchId";
                _connection.Execute(query, entity);
            }
        }

        public void Delete(long id)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                const string query = "DELETE FROM Searches WHERE SearchId = @SearchId";
                _connection.Execute(query, new { SearchId = id });
            }
        }
    }
}