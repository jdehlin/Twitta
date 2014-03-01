using System.Collections.Generic;
using Dapper;
using Twitta.Website.Models;

namespace Twitta.Website.Models
{
    public partial class Tweet
    {
        [Editable(false)]
        public long Id
        {
            get { return TweetId; }
            set { TweetId = value; }
        }

        public User User { get; set; }
        public List<HashTag> HashTags { get; set; } 
    }
}