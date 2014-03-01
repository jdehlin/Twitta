using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using TweetSharp;

namespace Twitta.Website.ViewModels
{
    public class SearchViewModel
    {
        public long SearchId { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string AllOfTheseWords { get; set; }
        [MaxLength(200)]
        public string ThisExactPhrase { get; set; }
        [MaxLength(200)]
        public string AnyOfTheseWords { get; set; }
        [MaxLength(200)]
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