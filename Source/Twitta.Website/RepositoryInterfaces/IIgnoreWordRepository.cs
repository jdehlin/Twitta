using System.Collections.Generic;
using Twitta.Website.Models;

namespace Twitta.Website.RepositoryInterfaces
{
    public interface IIgnoreWordRepository
    {
        int Insert(IgnoreWord item);
        IList<IgnoreWord> GetItems();
    }
}