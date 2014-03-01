using System.Collections.Generic;
using Twitta.Website.Models;

namespace Twitta.Website.RepositoryInterfaces
{
    public interface ITwitterAppRepository
    {
        IList<TwitterApp> GetItems();
        TwitterApp GetItem(int id);
        int Insert(TwitterApp entity);
        void Update(TwitterApp entity);
        void Delete(int id);
    }
}
