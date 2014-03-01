using System.Collections.Generic;

namespace Twitta.Website.RepositoryInterfaces
{
    public interface ISearchHistoryLogRepository
    {
        IList<SearchHistoryLog> GetItems(long searchId);
        IList<SearchHistoryLog> GetItems(IList<long> searchIds);
        int Insert(SearchHistoryLog entity);
    }
}
