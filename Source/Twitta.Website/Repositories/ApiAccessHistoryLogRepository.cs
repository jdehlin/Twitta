using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Repositories
{
    public class ApiAccessHistoryLogRepository : IApiAccessHistoryLogRepository
    {
        private DbConnection _connection;

        public IList<ApiAccessHistoryLog> GetItems(int apiApplicationId)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                var result = _connection.GetList<ApiAccessHistoryLog>(new { ApiApplicationId = apiApplicationId});
                return result.ToList();
            }
        }

        public int Insert(ApiAccessHistoryLog entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ApiAccessHistoryLog entity)
        {
            throw new NotImplementedException();
        }
    }
}