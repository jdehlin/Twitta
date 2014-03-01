using System.Collections.Generic;
using Twitta.Website.Models;

namespace Twitta.Website.ViewModels
{
    public class SearchReportView
    {
        public Search Search { get; set; }
        public IEnumerable<KeyValuePair<string, int>> Data { get; set; }
    }
}