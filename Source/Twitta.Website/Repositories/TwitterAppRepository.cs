using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Repositories
{
    public class TwitterAppRepository : ITwitterAppRepository
    {
        private DbConnection _connection;

        public IList<TwitterApp> GetItems()
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                var result = _connection.GetList<TwitterApp>();
                return result.ToList();
            }
        }

        public TwitterApp GetItem(int id)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                return _connection.Get<TwitterApp>(id);
            }
        }

        public void Update(TwitterApp entity)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                _connection.Update(entity);
            }
        }

        public void Delete(int id)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                _connection.Delete<TwitterApp>(id);
            }
        }

        public int Insert(TwitterApp entity)
        {
            using (_connection = Utilities.Database.GetProfiledOpenConnection())
            {
                return _connection.Insert(entity).Value;
            }
        }
    }
}