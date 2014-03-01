using System.Collections.Generic;

namespace Twitta.Website.RepositoryInterfaces
{
    public interface IIgnoreWordRepository
    {
        int Insert(IgnoreWord item);
        IList<IgnoreWord> GetItems();
    }
}