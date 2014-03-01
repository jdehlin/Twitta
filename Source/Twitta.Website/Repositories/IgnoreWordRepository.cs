using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Repositories
{
    public class IgnoreWordRepository : IIgnoreWordRepository
    {
        private DbConnection _connection;

        public int Insert(IgnoreWord item)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                const string query = "INSERT INTO IgnoreWords (IgnoreWordId, WordText) VALUES (@IgnoreWordId, @WordText)";
                return _connection.Execute(query, item);
            }
        }

        public IList<IgnoreWord> GetItems()
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                var result = _connection.GetList<IgnoreWord>();
                return result.ToList();
            }
        }
    }
}