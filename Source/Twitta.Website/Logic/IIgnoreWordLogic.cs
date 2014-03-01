using System.Collections.Generic;
using Twitta.Website.Models;

namespace Twitta.Website.Logic
{
    public interface IIgnoreWordLogic
    {
        IgnoreWord Insert(IgnoreWord item);
        IList<IgnoreWord> GetItems();
    }
}
