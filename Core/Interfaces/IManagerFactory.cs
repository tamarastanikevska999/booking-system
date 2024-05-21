using Core.DTO;


namespace Core.Interfaces
{
    public interface IManagerFactory
    {
        IManager CreateManager(SearchRequest request);
    }

    public interface IManager
    {
        Task<SearchResponse> Search(SearchRequest request);
        Task<BookResponse> Book(BookRequest request);
        Task<CheckStatusResponse> CheckStatus(CheckStatusRequest request);
    }
}
