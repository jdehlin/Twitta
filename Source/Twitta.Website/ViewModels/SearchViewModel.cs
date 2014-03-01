using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;

namespace Twitta.Website.ViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            Radius = 15;
        }

        public long SearchId { get; set; }
        public string Title { get; set; }
        public string AllOfTheseWords { get; set; }
        public string ThisExactPhrase { get; set; }
        public string AnyOfTheseWords { get; set; }
        public string NoneOfTheseWords { get; set; }
        public string NearThisPlace { get; set; }
        public int? Radius { get; set; }
        [Required]
        public TwitterSearchResultType ResultType { get; set; }

        public SelectList ResultTypeOptions
        {
            get
            {
                var statuses = from TwitterSearchResultType s in Enum.GetValues(typeof(TwitterSearchResultType))
                               select new { ID = s, Name = s.ToString() };
                return new SelectList(statuses, "ID", "Name");
            }
        }
    }
}