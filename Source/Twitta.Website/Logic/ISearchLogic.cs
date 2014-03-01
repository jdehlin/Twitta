using System;
using System.Collections.Generic;
using Twitta.Website.Models;

namespace Twitta.Website.Logic
{
    public interface ISearchLogic
    {
        IList<Search> GetItems(SearchDependencies? dependencies = null);
        Search GetItem(long id, SearchDependencies? dependencies = null);
        Search Insert(Search entity);
        Search Insert(Search entity, long lastTweetId, DateTime searchDateTime);
        Search Update(Search entity);
        void Delete(long id);
    }
}
