using System.Collections.Generic;
using Twitta.Website.Models;

namespace Twitta.Website.RepositoryInterfaces
{
    public interface IApiAccessHistoryLogRepository
    {
        IList<ApiAccessHistoryLog> GetItems(int apiApplicationId);
        int Insert(ApiAccessHistoryLog entity);
        void Update(ApiAccessHistoryLog entity);
    }
}
