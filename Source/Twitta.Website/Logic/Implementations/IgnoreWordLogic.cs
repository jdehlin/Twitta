using System.Collections.Generic;
using Twitta.Website.Models;
using Twitta.Website.RepositoryInterfaces;

namespace Twitta.Website.Logic.Implementations
{
    public class IgnoreWordLogic : IIgnoreWordLogic
    {
        private readonly IIgnoreWordRepository _ignoreWordRepository;

        public IgnoreWordLogic(IIgnoreWordRepository ignoreWordRepository)
        {
            _ignoreWordRepository = ignoreWordRepository;
        }

        public IgnoreWord Insert(IgnoreWord item)
        {
            item.IgnoreWordId = _ignoreWordRepository.Insert(item);
            return item;
        }

        public IList<IgnoreWord> GetItems()
        {
            return _ignoreWordRepository.GetItems();
        }
    }
}