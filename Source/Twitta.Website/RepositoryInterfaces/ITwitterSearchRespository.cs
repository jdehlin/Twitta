namespace Twitta.Website.RepositoryInterfaces
{
    public interface ITwitterSearchRepository
    {
        SearchResultWithRateLimit Search(SearchOptions options, TwitterApp keys);
        SearchResultWithRateLimit Search(string query, long sinceid, TwitterApp keys);

        SearchResultWithRateLimit Search(string queryTerm, int resultType, long sinceId, TwitterApp keys);


    }
}
