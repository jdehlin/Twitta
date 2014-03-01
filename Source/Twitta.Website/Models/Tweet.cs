using System.Collections.Generic;
using System.Linq;
using Dapper;
using Twitta.Website.Logic.Implementations;

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

        private SentimentOptions? _sentiment = null;
        public SentimentOptions? Sentiment
        {
            get
            {
                if (_sentiment != null) return _sentiment;
                if (string.IsNullOrWhiteSpace(Text)) _sentiment = SentimentOptions.Neutral;
                var charsToRemove = new[] { "@", ",", ".", ";", "'", ":", "#", "?", "!" };
                foreach (var c in charsToRemove)
                {
                    Text = Text.Replace(c, string.Empty);
                }
                var arrWords = Text.Split(' ').ToArray();
                arrWords = arrWords.Where(val => val != "a" && val != "the").ToArray();
                var total = 0;
                for (var i = 0; i < arrWords.Count(); i++)
                {
                    var currentWord = arrWords[i].Trim().ToLower();
                    if (SentimentWords.WordList.ContainsKey(currentWord))
                    {
                        if (currentWord == "not" && i < SentimentWords.WordList.Count - 1)
                        {
                            var nextWord = arrWords[i + 1].Trim().ToLower();
                            if (SentimentWords.WordList.ContainsKey(nextWord))
                            {
                                if (SentimentWords.WordList[nextWord] == SentimentOptions.Negative) total += (int)SentimentOptions.Positive;
                                if (SentimentWords.WordList[nextWord] == SentimentOptions.Positive) total += (int)SentimentOptions.Negative;
                                i++;
                            }
                        }
                        else
                        {
                            total += (int)SentimentWords.WordList[currentWord];
                        }
                    }
                }
                if (total < 0) _sentiment = SentimentOptions.Negative;
                if (total > 0) _sentiment = SentimentOptions.Positive;
                if (total == 0) _sentiment = SentimentOptions.Neutral;
                return _sentiment.Value;
            }
        }
    }
}