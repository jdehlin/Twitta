using System.Collections.Generic;

namespace Twitta.Website.Logic.Implementations
{
    public enum SentimentOptions
    {
        Positive = 1,
        Negative = -1,
        Neutral = 0
    }

    public static class SentimentWords
    {
        public static Dictionary<string, SentimentOptions> WordList = new Dictionary<string, SentimentOptions>()
        {
            {"awesome", SentimentOptions.Positive},
            {"awesomeness", SentimentOptions.Positive},
            {"great", SentimentOptions.Positive},
            {"good", SentimentOptions.Positive},
            {"cool", SentimentOptions.Positive},
            {"fun", SentimentOptions.Positive},
            {"best", SentimentOptions.Positive},
            {"amazing", SentimentOptions.Positive},
            {"yes", SentimentOptions.Positive},
            {"yeah", SentimentOptions.Positive},
            {"superb", SentimentOptions.Positive},
            {"bad", SentimentOptions.Negative},
            {"awful", SentimentOptions.Negative},
            {"horrible", SentimentOptions.Negative},
            {"suck", SentimentOptions.Negative},
            {"sucks", SentimentOptions.Negative},
            {"sucky", SentimentOptions.Negative},
            {"worst", SentimentOptions.Negative},
            {"not", SentimentOptions.Negative},
            {"no", SentimentOptions.Negative},
            {"shit", SentimentOptions.Negative},
            {"nasty", SentimentOptions.Negative}
        };
    }
}