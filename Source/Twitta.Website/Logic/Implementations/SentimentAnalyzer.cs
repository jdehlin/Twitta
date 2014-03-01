using System.Collections.Generic;
using System.Linq;

namespace Twitta.Website.Logic.Implementations
{
    public enum SentimentOptions
    {
        Positive = 1,
        Negative = -1,
        Neutral = 0
    }

    public class SentimentAnalyzer : ISentimentAnalyzer
    {
        private readonly Dictionary<string, SentimentOptions> Words = new Dictionary<string, SentimentOptions>();

        public SentimentAnalyzer()
        {
            Words.Add("awesome", SentimentOptions.Positive);
            Words.Add("great", SentimentOptions.Positive);
            Words.Add("good", SentimentOptions.Positive);
            Words.Add("cool", SentimentOptions.Positive);
            Words.Add("fun", SentimentOptions.Positive);
            Words.Add("best", SentimentOptions.Positive);
            Words.Add("amazing", SentimentOptions.Positive);
            Words.Add("bad", SentimentOptions.Negative);
            Words.Add("awful", SentimentOptions.Negative);
            Words.Add("horrible", SentimentOptions.Negative);
            Words.Add("sucks", SentimentOptions.Negative);
            Words.Add("worst", SentimentOptions.Negative);
        }

        public SentimentOptions Analyze(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return SentimentOptions.Neutral;
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", ":", "#", "?", "!" };
            foreach (var c in charsToRemove)
            {
                text = text.Replace(c, string.Empty);
            }
            var arrWords = text.Split(' ').ToArray();
            var total = 0;
            foreach (var word in arrWords)
            {
                var currentWord = word.Trim().ToLower();
                if (Words.ContainsKey(currentWord))
                {
                    total += (int) Words[currentWord];
                }
            }
            if (total < 0) return SentimentOptions.Negative;
            if (total > 0) return SentimentOptions.Positive;
            return SentimentOptions.Neutral;
        }
    }
}