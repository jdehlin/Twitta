using Twitta.Website.Logic.Implementations;

namespace Twitta.Website.Logic
{
    public interface ISentimentAnalyzer
    {
        SentimentOptions Analyze(string text);
    }
}