using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twitta.Website.Models;

namespace Twitta.Website.ViewModels
{
    public class SearchReportView
    {
        public Search Search { get; set; }
        public IEnumerable<KeyValuePair<string, int>> Data { get; set; }
    }
}