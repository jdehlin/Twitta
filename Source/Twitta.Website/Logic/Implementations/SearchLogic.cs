using System;
using System.Collections.Generic;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Logic.Implementations
{
    public class SearchLogic : ISearchLogic
    {
        private readonly ISearchRepository _searchRepository;
        private readonly ISearchHistoryLogRepository _searchHistoryLogRepository;

        public SearchLogic(ISearchRepository searchRepository, ISearchHistoryLogRepository searchHistoryLogRepository)
        {
            _searchRepository = searchRepository;
            _searchHistoryLogRepository = searchHistoryLogRepository;
        }

        public IList<Search> GetItems(SearchDependencies? dependencies = null)
        {
            return _searchRepository.GetItems(dependencies);
        }

        public Search GetItem(long id, SearchDependencies? dependencies = null)
        {
            return _searchRepository.GetItem(id, dependencies);
        }

        public Search Insert(Search entity)
        {
            if (entity == null) return null;
            entity.SearchId = _searchRepository.Insert(entity);
            return entity;
        }

        public Search Insert(Search entity, long lastTweetId, DateTime searchDateTime)
        {
            Insert(entity);
            if (entity.SearchId != 0)
            {
                _searchHistoryLogRepository.Insert(new SearchHistoryLog {SearchId = entity.SearchId, LastTweetId = lastTweetId, SearchDate = searchDateTime});
            }
            return entity;
        }

        public Search Update(Search entity)
        {
            if (entity == null) return null;
            _searchRepository.Update(entity);
            return entity;
        }

        public void Delete(long id)
        {
            _searchRepository.Delete(id);
        }
    }
}