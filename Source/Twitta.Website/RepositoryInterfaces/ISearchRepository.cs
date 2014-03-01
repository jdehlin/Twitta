﻿using System.Collections.Generic;

namespace Twitta.Website.RepositoryInterfaces
{
    public interface ISearchRepository
    {
        IList<Search> GetItems(SearchDependencies? dependencies = null);
        Search GetItem(long id, SearchDependencies? dependencies = null);
        int Insert(Search entity);
        void Update(Search entity);
        void Delete(long id);
    }
}
