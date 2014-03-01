using System.Collections.Generic;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Logic.Implementations
{
    public class TwitterAppLogic : ITwitterAppLogic
    {
        private readonly ITwitterAppRepository _apiApplicationRepository;

        public TwitterAppLogic(ITwitterAppRepository apiApplicationRepository)
        {
            _apiApplicationRepository = apiApplicationRepository;
        }

        public IEnumerable<TwitterApp> GetItems()
        {
            return _apiApplicationRepository.GetItems();
        }

        public TwitterApp GetItem(int id)
        {
            return _apiApplicationRepository.GetItem(id);
        }

        public TwitterApp SaveOrUpdate(TwitterApp entity)
        {
            if (entity.ApiApplicationId == 0)
            {
                entity.ApiApplicationId = _apiApplicationRepository.Insert(entity);
            }
            else
            {
                _apiApplicationRepository.Update(entity);
            }
            return entity;
        }

        public TwitterApp Delete(int id)
        {
            var entity = _apiApplicationRepository.GetItem(id);
            if (entity != null)
            {
                _apiApplicationRepository.Delete(entity.ApiApplicationId);
            }
            return entity;
        }
    }
}