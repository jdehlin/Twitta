using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitta.Website.Models
{
    public enum SearchDependencies
    {
        Logs
    }

    public partial class Search
    {
        public Search()
        {
            Logs = new List<SearchHistoryLog>();
            Tweets = new List<Tweet>();
        }

        public IList<SearchHistoryLog> Logs { get; set; }
        public SearchHistoryLog LastLog { get; set; }
        public IList<Tweet> Tweets { get; set; } 

        public int NumberOfTimes(int minutesInterval)
        {
            if (Logs == null || !Logs.Any()) return 0;
            return Logs.Count(l => l.SearchDate.AddMinutes(minutesInterval) > DateTime.UtcNow);
        }

        public string ToQuery()
        {
            var sb = new StringBuilder();
            sb.Append(string.IsNullOrWhiteSpace(AllOfTheseWords) ? string.Empty : AllOfTheseWords.Trim());
            sb.Append(string.IsNullOrWhiteSpace(ThisExactPhrase) ? string.Empty : " \"" + ThisExactPhrase + "\"");
            if (!string.IsNullOrWhiteSpace(AnyOfTheseWords))
            {
                var anyOfThese = AnyOfTheseWords.Split(' ');
                for (var i = 0; i < anyOfThese.Length; i++)
                {
                    if (i + 1 == anyOfThese.Length)
                    {
                        sb.Append(string.IsNullOrWhiteSpace(anyOfThese[i]) ? string.Empty : " " + anyOfThese[i]);
                    }
                    else
                    {
                        sb.Append(string.IsNullOrWhiteSpace(anyOfThese[i]) ? string.Empty : " " + anyOfThese[i] + " OR");
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(NoneOfTheseWords))
            {
                var noneOfThese = NoneOfTheseWords.Split(' ');
                foreach (var word in noneOfThese)
                {
                    sb.Append(string.IsNullOrWhiteSpace(word) ? string.Empty : " -" + word);
                }
            }
            if (!string.IsNullOrWhiteSpace(NearThisPlace))
            {
                sb.Append(" near:\"" + NearThisPlace + "\"");
            }
            if (Radius != null)
                sb.Append(" within:" + Radius + "mi");
            return sb.ToString();
        }
    }
}