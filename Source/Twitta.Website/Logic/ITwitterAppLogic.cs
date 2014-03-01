using System.Collections.Generic;
using Twitta.Website.Models;

namespace Twitta.Website.Logic
{
    public interface ITwitterAppLogic
    {
        IEnumerable<TwitterApp> GetItems();
        TwitterApp GetItem(int id);
        TwitterApp SaveOrUpdate(TwitterApp entity);
        TwitterApp Delete(int id);
    }
}